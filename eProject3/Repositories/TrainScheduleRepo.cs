using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics;

namespace eProject3.Repositories
{
    public class TrainScheduleRepo : ITrainScheduleRepo
    {
        private readonly DatabaseContext db;
        public TrainScheduleRepo(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<List<Train_Schedule>> DayMaster()
        {
            var today = DateTime.Now;
            var dayMaster = await db.Train_Schedules.Where(ts => ts.Time_begin <= today && today <= ts.Time_end).ToListAsync();
            return dayMaster;
        }
        public async Task<Train_Schedule> CreateSchedule(Train_Schedule train_Schedule)
        {
            try
            {
                var pass = new List<int>();
                for (int i = train_Schedule.Station_Code_begin; i <= train_Schedule.Station_code_end; i++)
                {
                    pass.Add(i);
                }
                var passString = string.Join(",", pass);
                var newSchedule = new Train_Schedule
                {
                    TrainId = train_Schedule.TrainId,
                    Station_Code_begin = train_Schedule.Station_Code_begin,
                    Station_code_end = train_Schedule.Station_code_end,
                    Station_code_pass = passString,
                    Direction = train_Schedule.Direction,
                    Route = train_Schedule.Route,
                    DetailID = train_Schedule.TrainId,
                    Time_begin = train_Schedule.Time_begin,
                    Time_end = train_Schedule.Time_end
                };

                db.Train_Schedules.Add(newSchedule);
                await db.SaveChangesAsync();

                var schedetail = await db.Train_Schedules
                                .Include(ts => ts.Detail)
                                .FirstAsync(ts => ts.Id == train_Schedule.DetailID);

                for (int i = train_Schedule.Station_Code_begin; i < train_Schedule.Station_code_end; i++)
                {
                    int currentStationBegin = train_Schedule.Station_Code_begin + i;
                    int currentStationEnd = train_Schedule.Station_Code_begin + i + 1;

                    int numOfRes = await db.SeatDetails
                        .Where(sd => sd.Status == "Reserved"
                            && currentStationBegin <= sd.Station_code_begin
                            && sd.Station_code_end <= currentStationEnd)
                        .CountAsync();

                    int totalSeats = await db.Coaches
                        .Where(c => c.TrainId == train_Schedule.TrainId)
                        .SumAsync(c => c.SeatsNumber);

                    int numOfVac = totalSeats - numOfRes;

                    schedetail.Detail.Add(new Train_Schedule_Detail
                    {
                        Station_Code_begin = currentStationBegin,
                        Station_code_end = currentStationEnd,
                        Seat_reserved = numOfRes,
                        Seat_vacant = numOfVac
                    });
                }

                // Save the changes to the database
                await db.SaveChangesAsync();
                /*var NSchDetail = new Train_Schedule_Detail
                {
                    Train_ScheduleId = train_Schedule.Id,
                    Station_Code_begin = train_Schedule.Station_Code_begin + 1,
                    Station_code_end = train_Schedule.Station_Code_begin + 1,

                }*/


                return newSchedule; // Return the newly created schedule
            }
            catch (Exception ex)
            {
                // Rethrow the caught exception to preserve the stack trace
                throw;
            }
        }


        public async Task<Train_Schedule> DeleteSchedule(int id)
        {
            try
            {
                var oldSche = await GetScheduleById(id);
                if (oldSche != null)
                {
                    db.Train_Schedules.Remove(oldSche);
                    await db.SaveChangesAsync();
                    return oldSche;
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

        public async Task<IEnumerable<Train_Schedule>> GetSchedule()
        {
            return await db.Train_Schedules.ToListAsync();
        }

        public async Task<Train_Schedule> GetScheduleById(int id)
        {
            try
            {
                return await db.Train_Schedules.SingleOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        

        public async Task<List<Train_Schedule>> Booking(int fromStation, int toStation, DateTime travelTime)
        {
            try
            {
                if (fromStation < toStation)
                {
                    string bookDirect = "down";
                    return await db.Train_Schedules.Where(ts => ts.Station_Code_begin <= fromStation &&
                                                                ts.Station_Code_begin <= toStation &&
                                                                ts.Station_code_end >= fromStation &&
                                                                ts.Station_code_end >= toStation &&
                                                                ts.Time_begin <= travelTime &&
                                                                ts.Time_end >= travelTime &&
                                                                ts.Direction == bookDirect).ToListAsync();
                }
                if (fromStation > toStation)
                {
                    string bookDirect = "up";
                    return await db.Train_Schedules.Where(ts => ts.Station_Code_begin >= fromStation &&
                                                                ts.Station_Code_begin >= toStation &&
                                                                ts.Station_code_end <= fromStation &&
                                                                ts.Station_code_end <= toStation &&
                                                                ts.Time_begin <= travelTime &&
                                                                ts.Time_end >= travelTime &&
                                                                ts.Direction == bookDirect).ToListAsync();
                }
                return new List<Train_Schedule>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Train_Schedule> UpdateSchedule(Train_Schedule train_Schedule)
        {
            var oldSche = await GetScheduleById(train_Schedule.Id);
            if (oldSche != null)
            {
                var userType = typeof(Train);
                foreach (var property in userType.GetProperties())
                {
                    var newValue = property.GetValue(train_Schedule);
                    if (newValue != null && !string.IsNullOrWhiteSpace(newValue.ToString()))
                    {
                        property.SetValue(oldSche, newValue);
                    }
                }
                db.Train_Schedules.Update(oldSche);
                await db.SaveChangesAsync();
                return oldSche;
            }
            else
            {
                throw new ArgumentException("No ID found");
            }
        }

        
    }
}
