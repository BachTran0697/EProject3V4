using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

namespace eProject3.Repositories
{
    public class TrainRepo : ITrainRepo
    {
        private readonly DatabaseContext db;
        public TrainRepo(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<Train> CreateTrain(Train train)
        {
            try
            {
                db.Trains.Add(train);
                await db.SaveChangesAsync();
                return train;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Train> DeleteTrain(int id)
        {
            try
            {
                var oldTrain = await GetTrainById(id);
                if (oldTrain != null)
                {
                    db.Trains.Remove(oldTrain);
                    await db.SaveChangesAsync();
                    return oldTrain;
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

        public async Task<Train> GetTrainById(int id)
        {
            try
            {
                return await db.Trains.SingleOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Train>> GetTrains()
        {
            return await db.Trains.ToListAsync();
        }

        public async Task<Train> UpdateTrain(Train train)
        {
            var oldTrain = await GetTrainById(train.Id);
            if (oldTrain != null)
            {
                var userType = typeof(Train);
                foreach (var property in userType.GetProperties())
                {
                    var newValue = property.GetValue(train);
                    if (newValue != null && !string.IsNullOrWhiteSpace(newValue.ToString()))
                    {
                        property.SetValue(oldTrain, newValue);
                    }
                }
                db.Trains.Update(oldTrain);
                await db.SaveChangesAsync();
                return oldTrain;
            }
            else
            {
                throw new ArgumentException("No ID found");
            }
        }
    }
}
