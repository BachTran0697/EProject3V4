using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

namespace eProject3.Repositories
{
    public class CancelRepo : ICancelRepo
    {
        private readonly DatabaseContext db;
        public CancelRepo(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<CancelResponse> CancelReservationAsync(string ticketCode, string email, string phone)
        {
            var reservation = await db.Reservations.FirstOrDefaultAsync(r => r.Ticket_code == ticketCode &&
                                                                             (r.Email == email || r.Phone == phone));

            if (reservation == null)
            {
                throw new ArgumentException("Reservation not found.");
            }

            if (reservation.IsCancelled)
            {
                throw new ArgumentException("Reservation already cancelled.");
            }

            if (reservation.Time_begin < DateTime.Now)
            {
                throw new ArgumentException("Journey date has already expired.");
            }

            var cancellationFee = CalculateCancellationFee(reservation);

            reservation.IsCancelled = true;
            reservation.CancellationFee = cancellationFee;
            reservation.CancelledDate = DateTime.Now;

            db.Reservations.Update(reservation);

            // Tìm tất cả SeatDetails liên quan đến Seat của Reservation và cập nhật trạng thái của chúng thành "free"
            var seatDetails = await db.SeatDetails
                .Where(sd => sd.SeatId == reservation.Seat_id)
                .ToListAsync();

            foreach (var seatDetail in seatDetails)
            {
                seatDetail.Status = "free";
                db.SeatDetails.Update(seatDetail);
            }
            var coach = await db.Coaches.FindAsync(reservation.Coach_id);
            if (coach == null)
            {
                throw new InvalidOperationException("No coach found with the provided Coach ID.");
            }
            // Cập nhật thông tin cho coach
            coach.Seats_reserved--;
            coach.Seats_vacant++;
            db.Entry(coach).State = EntityState.Modified;

            // Lưu thay đổi vào cơ sở dữ liệu
            await db.SaveChangesAsync();

            return new CancelResponse
            {
                ReservationDetails = reservation,
                CancellationFee = cancellationFee
            };
        }

        private decimal CalculateCancellationFee(Reservation reservation)
        {
            // Kiểm tra xem giá vé có null hay không
            if (!reservation.Price.HasValue)
            {
                throw new InvalidOperationException("Price cannot be null.");
            }

            var timeToDeparture = reservation.Time_begin - DateTime.Now;

            if (timeToDeparture.TotalHours > 24)
            {
                // Hoàn 90% giá vé nếu thời gian trả vé trên 24 giờ
                return reservation.Price.Value * 0.10m;
            }
            else if (timeToDeparture.TotalHours >= 0)
            {
                // Hoàn 50% giá vé nếu thời gian trả vé từ 24 giờ đến trước giờ khởi hành
                return reservation.Price.Value * 0.50m;
            }
            else
            {
                // Không hoàn tiền nếu thời gian trả vé sau giờ khởi hành
                return reservation.Price.Value;
            }
        }
        public async Task<IEnumerable<Reservation>> GetCancellations()
        {
            var cancelledReserv = await db.Reservations.Where(r => r.IsCancelled == true).ToListAsync();
            return cancelledReserv;
        }
    }
}
