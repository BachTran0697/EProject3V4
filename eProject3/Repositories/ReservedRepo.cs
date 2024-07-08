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
        public async Task<Reservation> CreateReservation(Reservation reservation)
        {
            // Kiểm tra tính khả dụng của chỗ ngồi
            var seatAvailable = await db.Seats
                .Where(s => s.Id == reservation.Seat_id && s.CoachId == reservation.Coach_id)
                .FirstOrDefaultAsync(s => s.SeatDetails.Any(sd => sd.Status == "free"));

            if (seatAvailable == null)
            {
                throw new Exception("Seat is not available.");
            }

            // Tạo mã vé
            reservation.Ticket_code = GenerateTicketCode();

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

            return reservation;
        }

        private string GenerateTicketCode()
        {
            return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
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
