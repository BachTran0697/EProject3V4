using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.EntityFrameworkCore;

namespace eProject3.Repositories
{
    public class SeatRepo : ISeatRepo
    {
        private readonly DatabaseContext db;
        public SeatRepo(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<Seat> CreateSeat(Seat seat)
        {
            try
            {
                db.Seats.Add(seat);
                await db.SaveChangesAsync();
                return seat;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Seat> DeleteSeat(int id)
        {
            try
            {
                var oldSeat = await GetSeatById(id);
                if (oldSeat != null)
                {
                    db.Seats.Remove(oldSeat);
                    await db.SaveChangesAsync();
                    return oldSeat;
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

        public async Task<Seat> GetSeatById(int id)
        {
            try
            {
                return await db.Seats.SingleOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Seat>> GetSeats()
        {
            return await db.Seats.ToListAsync();
        }

        public async Task<Seat> UpdateSeat(Seat seat)
        {
            var oldSeat = await GetSeatById(seat.Id);
            if (oldSeat != null)
            {
                var userType = typeof(Seat);
                foreach (var property in userType.GetProperties())
                {
                    var newValue = property.GetValue(seat);
                    if (newValue != null && !string.IsNullOrWhiteSpace(newValue.ToString()))
                    {
                        property.SetValue(oldSeat, newValue);
                    }
                }
                db.Seats.Update(oldSeat);
                await db.SaveChangesAsync();
                return oldSeat;
            }
            else
            {
                throw new ArgumentException("No ID found");
            }
        }
        public async Task<IEnumerable<Seat>> GetSeatsByCoachId(int coachId)
        {
            try
            {
                return await db.Seats.Where(c => c.CoachId == coachId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
