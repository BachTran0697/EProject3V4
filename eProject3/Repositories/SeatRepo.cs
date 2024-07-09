using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProject3.Repositories
{
    public class SeatRepo : ISeatRepo
    {
        private readonly DatabaseContext db;

        public SeatRepo(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Seat>> GetSeats()
        {
            return await db.Seats.ToListAsync();
        }

        public async Task<IEnumerable<Seat>> GetSeatsByCoachId(int coachId)
        {
            return await db.Seats
                .Where(s => s.CoachId == coachId)
                .Include(s => s.SeatDetails)
                .ToListAsync();
        }

        public async Task<Seat> CreateSeat(Seat seat)
        {
            db.Seats.Add(seat);
            await db.SaveChangesAsync();
            return seat;
        }

        public async Task<Seat> UpdateSeat(Seat seat)
        {
            db.Seats.Update(seat);
            await db.SaveChangesAsync();
            return seat;
        }

        public async Task<Seat> DeleteSeat(int id)
        {
            var seat = await db.Seats.FindAsync(id);
            if (seat != null)
            {
                db.Seats.Remove(seat);
                await db.SaveChangesAsync();
            }
            return seat;
        }
    }
}
