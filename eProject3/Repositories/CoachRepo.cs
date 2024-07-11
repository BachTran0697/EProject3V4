using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

                return coach;
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // LogError(ex);
                throw new Exception("An error occurred while creating the coach and its seats: " + ex.Message, ex);
            }
        }

        public async Task<bool> DeleteCoach(int coachId)
        {
            try
            {
                var coach = await db.Coaches
                    .Include(c => c.Seats)
                    .FirstOrDefaultAsync(c => c.Id == coachId);

                if (coach == null)
                {
                    return false; // Coach không tồn tại
                }

                // Xóa các SeatDetail liên quan đến các Seat
                var seatIds = coach.Seats.Select(s => s.Id).ToList();
                var seatDetails = await db.SeatDetails.Where(sd => seatIds.Contains(sd.SeatId)).ToListAsync();
                db.SeatDetails.RemoveRange(seatDetails);

                // Xóa các Seat
                db.Seats.RemoveRange(coach.Seats);

                // Xóa Coach
                db.Coaches.Remove(coach);

                // Lưu các thay đổi vào cơ sở dữ liệu
                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // LogError(ex);
                throw new Exception("An error occurred while deleting the coach and its seats: " + ex.Message, ex);
            }
        }
        private bool ConfirmDeleteWithUser()
        {
            Console.WriteLine("Are you sure you want to delete this coach and all its related seats? (yes/no):");
            var response = Console.ReadLine();
            return response?.ToLower() == "yes";
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
        public async Task<Coach> UpdateCoach(Coach updatedCoach)
        {
            try
            {
                // Tìm Coach hiện tại trong cơ sở dữ liệu
                var existingCoach = await db.Coaches
                    .Include(c => c.Seats)
                    .FirstOrDefaultAsync(c => c.Id == updatedCoach.Id);

                if (existingCoach == null)
                {
                    throw new Exception("Coach không tồn tại.");
                }

                // Cập nhật thông tin của Coach
                existingCoach.CoachNumber = updatedCoach.CoachNumber;
                existingCoach.SeatsNumber = updatedCoach.SeatsNumber;
                existingCoach.TrainId = updatedCoach.TrainId;

                // Xóa các Seat hiện tại nếu số lượng ghế thay đổi
                if (existingCoach.SeatsNumber != updatedCoach.SeatsNumber)
                {
                    var existingSeatIds = existingCoach.Seats.Select(s => s.Id).ToList();
                    var existingSeatDetails = await db.SeatDetails.Where(sd => existingSeatIds.Contains(sd.SeatId)).ToListAsync();

                    // Xóa các SeatDetail liên quan đến các Seat
                    db.SeatDetails.RemoveRange(existingSeatDetails);

                    // Xóa các Seat hiện tại
                    db.Seats.RemoveRange(existingCoach.Seats);

                    // Tạo lại các Seat mới
                    var newSeats = new List<Seat>();
                    for (int i = 1; i <= updatedCoach.SeatsNumber; i++)
                    {
                        var seatName = updatedCoach.CoachNumber + "Seat" + i;
                        newSeats.Add(new Seat { CoachId = existingCoach.Id, SeatNumber = seatName });
                    }
                    db.Seats.AddRange(newSeats);
                }

                // Lưu các thay đổi vào cơ sở dữ liệu
                await db.SaveChangesAsync();

                return existingCoach;
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // LogError(ex);
                throw new Exception("An error occurred while updating the coach and its seats: " + ex.Message, ex);
            }
        }

    }
}

