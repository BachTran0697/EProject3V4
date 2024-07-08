using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

namespace eProject3.Repositories
{
    public class CoachRepo : ICoachRepo
    {
        private readonly DatabaseContext db;
        public CoachRepo(DatabaseContext db)
        {
            this.db = db;
        }
        /*public async Task<Coach> CreateCoach(Coach coach)
        {
            try
            {
                db.Coaches.Add(coach);
                await db.SaveChangesAsync();
                var numSeats = coach.SeatsNumber;
                var seats = new List<Seat>();
                var seatDetails = new List<SeatDetail>();

                for (int i = 1; i <= numSeats; i++)
                {
                    var seatName = coach.CoachNumber + "Seat" + i;
                    seats.Add(new Seat { CoachId = coach.Id, SeatNumber = seatName });
                }

                db.Seats.AddRange(seats);
                await db.SaveChangesAsync();

                for (int i = 1;i<=numSeats;i++)
                {
                    var seatName = coach.CoachNumber + "Seat" + i;
                    var seat = await db.Seats.FirstOrDefaultAsync(s => s.SeatNumber == seatName);

                    if (seat != null)
                    {
                        var detailId = seat.Id;
                        seatDetails.Add(new SeatDetail
                        {
                            SeatId = detailId,
                            Station_code_begin = 1,
                            Station_code_end = 2,
                            Status = "free"
                        });
                    }
                    else
                    {
                        // Handle case where seat is not found (optional)
                        throw new Exception($"Seat {seatName} not found.");
                    }
                }
                db.SeatDetails.AddRange(seatDetails);
                await db.SaveChangesAsync();
                return coach;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
*/
        public async Task<Coach> CreateCoach(Coach coach)
        {
            try
            {
                // Add and save the coach entity
                db.Coaches.Add(coach);
                await db.SaveChangesAsync();

                var numSeats = coach.SeatsNumber;
                var seats = new List<Seat>();
                var seatDetails = new List<SeatDetail>();

                // Create Seat entities
                for (int i = 1; i <= numSeats; i++)
                {
                    var seatName = coach.CoachNumber + "Seat" + i;
                    seats.Add(new Seat { CoachId = coach.Id, SeatNumber = seatName });
                }

                // Add and save Seat entities
                db.Seats.AddRange(seats);
                await db.SaveChangesAsync();

                // Fetch the seats again after saving them to get their IDs
                /*for (int i = 1; i <= numSeats; i++)
                {
                    var seatName = coach.CoachNumber + "Seat" + i;
                    var seat = await db.Seats.FirstOrDefaultAsync(s => s.SeatNumber == seatName && s.CoachId == coach.Id);

                    var detailId = seat.Id;
                    seatDetails.Add(new SeatDetail
                    {
                        SeatId = detailId,
                        Station_code_begin = 1,
                        Station_code_end = 2,
                        Status = "free"
                    });
                }

                // Add and save SeatDetail entities
                db.SeatDetails.AddRange(seatDetails);
                await db.SaveChangesAsync();
*/
                return coach;
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // LogError(ex);
                throw new Exception("An error occurred while creating the coach and its seats: " + ex.Message, ex);
            }
        }

        public async Task<Coach> DeleteCoach(int id)
        {
            try
            {
                var oldCoach = await GetCoachById(id);
                if (oldCoach != null)
                {
                    db.Coaches.Remove(oldCoach);
                    await db.SaveChangesAsync();
                    return oldCoach;
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

        public async Task<Coach> GetCoachById(int id)
        {
            try
            {
                return await db.Coaches.SingleOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Coach>> GetCoachs()
        {
            return await db.Coaches.ToListAsync();
        }

        public async Task<Coach> UpdateCoach(Coach coach)
        {
            var oldCoach = await GetCoachById(coach.Id);
            if (oldCoach != null)
            {
                var userType = typeof(Coach);
                foreach (var property in userType.GetProperties())
                {
                    var newValue = property.GetValue(coach);
                    if (newValue != null && !string.IsNullOrWhiteSpace(newValue.ToString()))
                    {
                        property.SetValue(oldCoach, newValue);
                    }
                }
                db.Coaches.Update(oldCoach);
                await db.SaveChangesAsync();
                return oldCoach;
            }
            else
            {
                throw new ArgumentException("No ID found");
            }
        }
        public async Task<IEnumerable<Coach>> GetCoachesByTrainId(int trainId)
        {
            try
            {
                return await db.Coaches.Where(c => c.TrainId == trainId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

