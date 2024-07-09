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

        public async Task<CancelResponse> CancelReservationAsync(CancelRequest request)
        {
            var reservation = await db.Reservations.FirstOrDefaultAsync(r => r.Ticket_code == request.Ticket_Code);

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

            return new CancelResponse
            {
                ReservationDetails = reservation,
                CancellationFee = cancellationFee
            };
        }

        public async Task<string> ConfirmCancelAsync(ConfirmCancelRequest request)
        {
            var reservation = await db.Reservations.FirstOrDefaultAsync(r => r.Ticket_code == request.Ticket_Code);

            if (reservation == null || reservation.IsCancelled || reservation.Time_begin < DateTime.Now)
            {
                throw new ArgumentException("Invalid cancellation request.");
            }

            reservation.IsCancelled = true;
            reservation.CancellationFee = request.CancellationFee;
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

            // Lưu thay đổi vào cơ sở dữ liệu
            await db.SaveChangesAsync();

            return "Reservation cancelled successfully.";
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
        public async Task<Cancellation> CreateCancellation(Cancellation Cancellation)
        {
            try
            {
                db.Cancellations.Add(Cancellation);
                await db.SaveChangesAsync();
                return Cancellation;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cancellation> DeleteCancellation(int id)
        {
            try
            {
                var oldCancel = await GetCancellationById(id);
                if (oldCancel != null)
                {
                    db.Cancellations.Remove(oldCancel);
                    await db.SaveChangesAsync();
                    return oldCancel;
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

        public async Task<Cancellation> GetCancellationById(int id)
        {
            try
            {
                return await db.Cancellations.SingleOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Cancellation>> GetCancellations()
        {
            return await db.Cancellations.ToListAsync();
        }

        public async Task<Cancellation> UpdateCancellation(Cancellation Cancellation)
        {
            var oldCancel = await GetCancellationById(Cancellation.Id);
            if (oldCancel != null)
            {
                var userType = typeof(Cancellation);
                foreach (var property in userType.GetProperties())
                {
                    var newValue = property.GetValue(Cancellation);
                    if (newValue != null && !string.IsNullOrWhiteSpace(newValue.ToString()))
                    {
                        property.SetValue(oldCancel, newValue);
                    }
                }
                db.Cancellations.Update(oldCancel);
                await db.SaveChangesAsync();
                return oldCancel;
            }
            else
            {
                throw new ArgumentException("No ID found");
            }
        }
    }
}
