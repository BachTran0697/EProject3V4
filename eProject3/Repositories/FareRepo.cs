using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.EntityFrameworkCore;

namespace eProject3.Repositories
{
    public class FareRepo : IFaresRepo
    {
        private readonly DatabaseContext db;
        public FareRepo(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<Fares> CreateFare(Fares fare)
        {
            try
            {
                db.Fares.Add(fare);
                await db.SaveChangesAsync();
                return fare;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Fares> DeleteFare(int id)
        {
            try
            {
                var oldFare = await GetFareById(id);
                if (oldFare != null)
                {
                    db.Fares.Remove(oldFare);
                    await db.SaveChangesAsync();
                    return oldFare;
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

        public async Task<Fares> GetFareById(int id)
        {
            try
            {
                return await db.Fares.SingleOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Fares>> GetFares()
        {
            return await db.Fares.ToListAsync();
        }

        public async Task<Fares> UpdateFare(Fares fare)
        {
            var oldFare = await GetFareById(fare.Id);
            if (oldFare != null)
            {
                var userType = typeof(Fares);
                foreach (var property in userType.GetProperties())
                {
                    var newValue = property.GetValue(fare);
                    if (newValue != null && !string.IsNullOrWhiteSpace(newValue.ToString()))
                    {
                        property.SetValue(oldFare, newValue);
                    }
                }
                db.Fares.Update(oldFare);
                await db.SaveChangesAsync();
                return oldFare;
            }
            else
            {
                throw new ArgumentException("No ID found");
            }
        }
    }
}
