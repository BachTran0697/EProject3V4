using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProject3.Repositories
{
    public class ReservedRepo : IReservedRepo
    {
        private readonly DatabaseContext db;
        public ReservedRepo(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<Reservation> GetByPRNAsync(string prn)
        {
            return await db.Reservations.FirstOrDefaultAsync(r => r.Ticket_code == prn);
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            db.Reservations.Update(reservation);
            await db.SaveChangesAsync();
        }

        public async Task<(DateTime timeBegin, DateTime timeEnd)> ConfirmReserved(Reservation reservation)
        {
            var schedule = await CalculateSchedule(reservation);
            var timeBegin = schedule.Time_begin;
            var timeEnd = schedule.Time_end;
            return (timeBegin, timeEnd);
        }

        public async Task<Reservation> CreateReservation(Reservation reservation)
        {
            // Kiểm tra tính khả dụng của chỗ ngồi
            var seatAvailable = await db.Seats
                .Where(s => s.Id == reservation.Seat_id && s.CoachId == reservation.Coach_id)
                .FirstOrDefaultAsync(s => !s.SeatDetails.Any() || s.SeatDetails.Any(sd => sd.Status == "free"));

            if (seatAvailable == null)
            {
                throw new Exception("Seat is not available.");
            }

            // Tạo mã vé
            reservation.Ticket_code = GenerateTicketCode();

            // Tính toán Time_begin và Time_end dựa trên tốc độ của Train_id và khoảng cách giữa các Station_id
            var schedule = await CalculateSchedule(reservation);

            // Tạo reservation mới và thêm vào database
            var newReservation = new Reservation
            {
                Name = reservation.Name,
                Email = reservation.Email,
                Phone = reservation.Phone,
                Ticket_code = reservation.Ticket_code,
                Station_begin_id = reservation.Station_begin_id,
                Station_end_id = reservation.Station_end_id,
                Time_begin = schedule.Time_begin,
                Time_end = schedule.Time_end,
                Train_id = reservation.Train_id,
                Coach_id = reservation.Coach_id,
                Seat_id = reservation.Seat_id,
                Price = reservation.Price,
                IsCancelled = false
            };

            db.Reservations.Add(newReservation);
            await db.SaveChangesAsync();

            // Đánh dấu chỗ ngồi đã được đặt và tạo mới SeatDetail
            var seat = await db.Seats
                .Include(s => s.SeatDetails)
                .FirstAsync(s => s.Id == reservation.Seat_id);

            var newSeatDetail = new SeatDetail
            {
                Status = "Reserved",
                Station_code_begin = reservation.Station_begin_id,
                Station_code_end = reservation.Station_end_id,
                SeatId = seat.Id
            };

            db.SeatDetails.Add(newSeatDetail);
            await db.SaveChangesAsync();

            // Cập nhật thông tin cho coach
            var coach = await db.Coaches.FindAsync(reservation.Coach_id);
            if (coach != null)
            {
                coach.Seats_vacant--; // Giảm số ghế trống
                coach.Seats_reserved++; // Tăng số ghế đã đặt
                db.Entry(coach).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

            return newReservation;
        }

        private async Task<(DateTime Time_begin, DateTime Time_end)> CalculateSchedule(Reservation reservation)
        {
            var schedule = await db.Train_Schedules
                .Where(ts =>
                    ((ts.Station_Code_begin <= reservation.Station_begin_id &&
                    ts.Station_code_end >= reservation.Station_end_id) ||
                    (ts.Station_Code_begin >= reservation.Station_begin_id &&
                    ts.Station_code_end <= reservation.Station_end_id)) &&
                    ts.Time_begin >= reservation.Time_begin &&
                    ts.Time_begin <= reservation.Time_end)
                .FirstOrDefaultAsync();

            if (schedule == null)
            {
                throw new Exception("No suitable schedule found.");
            }

            // Tính toán thời gian bắt đầu và kết thúc dựa trên tốc độ và khoảng cách
            var startTime = await CalculateStartTime(schedule, reservation);
            var endTime = await CalculateEndTime(schedule, reservation);

            return (startTime, endTime);
        }

        private async Task<DateTime> CalculateStartTime(Train_Schedule schedule, Reservation reservation)
        {
            var train = await db.Trains.FindAsync(schedule.TrainId);
            if (train == null)
            {
                throw new Exception($"Train with id {schedule.TrainId} not found.");
            }

            int trainSpeed = int.Parse(train.Speed);

            // Retrieve stations from the database
            var staStart = db.Stations.FirstOrDefault(s => s.Id == schedule.Station_Code_begin);
            var staEnd = db.Stations.FirstOrDefault(s => s.Id == reservation.Station_begin_id);

            if (staStart == null || staEnd == null)
            {
                throw new Exception("Invalid station information.");
            }

            // Calculate distance between Station_Code_begin and Station_begin_id
            int distanceBegin = staEnd.distance - staStart.distance; // Assuming Distance is a property of the Station entity

            // Calculate start time based on speed and distance
            double hoursToAdd = distanceBegin / (double)trainSpeed; // Cast trainSpeed to double for accurate division
            DateTime startTime = schedule.Time_begin.AddHours(hoursToAdd);

            return startTime;
        }

        private async Task<DateTime> CalculateEndTime(Train_Schedule schedule, Reservation reservation)
        {
            var train = await db.Trains.FindAsync(schedule.TrainId);
            if (train == null)
            {
                throw new Exception($"Train with id {schedule.TrainId} not found.");
            }

            int trainSpeed = int.Parse(train.Speed);

            // Retrieve stations from the database
            var staStart = db.Stations.FirstOrDefault(s => s.Id == reservation.Station_begin_id);
            var staEnd = db.Stations.FirstOrDefault(s => s.Id == reservation.Station_end_id);

            if (staStart == null || staEnd == null)
            {
                throw new Exception("Invalid station information.");
            }

            // Calculate distance between Station_begin_id and Station_end_id
            int distanceEnd = staEnd.distance - staStart.distance; // Assuming Distance is a property of the Station entity

            // Calculate end time based on speed and distance
            double hoursToAdd = distanceEnd / (double)trainSpeed; // Cast trainSpeed to double for accurate division
            DateTime endTime = schedule.Time_end.AddHours(hoursToAdd); // Use schedule.Time_end here

            return endTime;
        }



        private string GenerateTicketCode()
        {
            // Lấy phần năm, tháng, ngày, giờ, phút, giây từ ngày giờ hiện tại
            string dateTimePart = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Tạo phần ngẫu nhiên
            string randomPart = Guid.NewGuid().ToString().Substring(0, 4).ToUpper();

            // Kết hợp cả hai phần lại
            return $"{dateTimePart}{randomPart}";
        }

        public async Task<List<Reservation>> FinishReservation()
        {
            try
            {
                // Lấy danh sách các reservation cần hoàn thành
                var reservations = await db.Reservations
                    .Where(r => r.Time_end < DateTime.Now) // Chỉ lấy những reservation chưa kết thúc
                    .ToListAsync();

                foreach (var reservation in reservations)
                {
                    // Lấy danh sách các seatDetails tương ứng với reservation.Seat_id và có status là "Reserved"
                    var seatDetails = await db.SeatDetails
                        .Where(sd => sd.SeatId == reservation.Seat_id && sd.Status == "Reserved")
                        .ToListAsync();

                    // Duyệt qua từng seatDetail tương ứng với reservation
                    foreach (var seatDetail in seatDetails)
                    {
                        var coach = await db.Coaches.FindAsync(reservation.Coach_id);
                        if (coach == null)
                        {
                            throw new InvalidOperationException($"No coach found with ID: {reservation.Coach_id}.");
                        }

                        // Cập nhật trạng thái SeatDetail
                        seatDetail.Status = "Finish";
                        db.SeatDetails.Update(seatDetail);

                        // Cập nhật thông tin cho coach
                        coach.Seats_reserved--;
                        coach.Seats_vacant++;
                        db.Entry(coach).State = EntityState.Modified;
                    }

                    // Cập nhật trạng thái của reservation
                    reservation.IsCancelled = true; // Hoặc có thể đánh dấu là đã hoàn thành tùy theo yêu cầu
                }

                await db.SaveChangesAsync();

                return reservations;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error finishing reservations: {ex.Message}", ex);
            }
        }




        public async Task<Reservation> GetReservationById(int id)
        {
            try
            {
                return await db.Reservations.SingleOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            return await db.Reservations.ToListAsync();
        }

        public async Task<Reservation> UpdateReservation(Reservation reservation)
        {
            var existingReservation = await GetReservationById(reservation.Id);
            if (existingReservation == null)
            {
                throw new ArgumentException("No reservation found with the provided ID.");
            }

            existingReservation.Name = reservation.Name ?? existingReservation.Name;
            existingReservation.Email = reservation.Email ?? existingReservation.Email;
            existingReservation.Phone = reservation.Phone ?? existingReservation.Phone;
            existingReservation.Station_begin_id = reservation.Station_begin_id;
            existingReservation.Station_end_id = reservation.Station_end_id;
            existingReservation.Time_begin = reservation.Time_begin;
            existingReservation.Time_end = reservation.Time_end;
            existingReservation.Train_id = reservation.Train_id;
            existingReservation.Coach_id = reservation.Coach_id;
            existingReservation.Seat_id = reservation.Seat_id;
            existingReservation.Price = reservation.Price;
            existingReservation.IsCancelled = reservation.IsCancelled;

            db.Reservations.Update(existingReservation);
            await db.SaveChangesAsync();

            return existingReservation;
        }
        
        public async Task<IEnumerable<Reservation>> GetReservationsByUserAsync(string name, string phone)
        {
            return await db.Reservations
                .Where(r => r.Name == name && r.Phone == phone)
                .ToListAsync();
        }

        public async Task<Reservation> PayCheck(int id)
        {
            var reservation = await GetReservationById(id);
            if (reservation == null)
            {
                return null;
            }

            reservation.PayStatus = "Paid";
            db.Reservations.Update(reservation);
            await db.SaveChangesAsync();

            return reservation;
        }

        
    }
}
