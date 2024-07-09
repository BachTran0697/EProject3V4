using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.EntityFrameworkCore;

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
            var nReserved = new Reservation
            {
                Name = reservation.Name,
                Email = reservation.Email,
                Phone = reservation.Phone,
                Ticket_code = reservation.Ticket_code,
                Station_begin_id = reservation.Station_begin_id,
                Station_end_id = reservation.Station_end_id,
                Time_begin=reservation.Time_begin,
                Time_end=reservation.Time_end,
                Train_id = reservation.Train_id,
                Coach_id = reservation.Coach_id,
                Seat_id = reservation.Seat_id,
                Price = reservation.Price,
                IsCancelled = false
            };
            db.Reservations.Add(nReserved);
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
            return reservation;
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

        public async Task<Reservation> FinishReservation(int id)
        {
            try
            {
                var oldReserved = await GetReservationById(id);
                if (oldReserved != null)
                {
                    if(oldReserved.Time_end > DateTime.Now)
                    {
                        // Lấy SeatDetail tương ứng
                        var seatDetail = await db.SeatDetails
                            .FirstOrDefaultAsync(sd => sd.SeatId == oldReserved.Seat_id && sd.Status == "Reserved");

                        if (seatDetail != null)
                        {
                            // Cập nhật trạng thái SeatDetail
                            seatDetail.Status = "Finish";
                            db.SeatDetails.Update(seatDetail);
                        }
                    }
                    
                    await db.SaveChangesAsync();
                    return oldReserved;
                }
                else
                {
                    throw new ArgumentException("No ID found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        public async Task<Reservation> UpdateReservation(Reservation Reservation)
        {
            var oldReserved = await GetReservationById(Reservation.Id);
            if (oldReserved != null)
            {
                var userType = typeof(Reservation);
                foreach (var property in userType.GetProperties())
                {
                    var newValue = property.GetValue(Reservation);
                    if (newValue != null && !string.IsNullOrWhiteSpace(newValue.ToString()))
                    {
                        property.SetValue(oldReserved, newValue);
                    }
                }
                db.Reservations.Update(oldReserved);
                await db.SaveChangesAsync();
                return oldReserved;
            }
            else
            {
                throw new ArgumentException("No ID found");
            }
        }
    }
}
