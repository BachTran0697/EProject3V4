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
