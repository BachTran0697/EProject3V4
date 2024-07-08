using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static System.Collections.Specialized.BitVector32;

namespace eProject3.Repositories
{
    public class StationRepo : IStationRepo
    {
        private readonly DatabaseContext db;
        public StationRepo(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<Station> CreateStation(Station station)
        {
            try
            {
                db.Stations.Add(station);
                await db.SaveChangesAsync();
                return station;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Station> DeleteStation(int id)
        {
            try
            {
                var oldStation = await GetStationById(id);
                if (oldStation != null)
                {
                    db.Stations.Remove(oldStation);
                    await db.SaveChangesAsync();
                    return oldStation;
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

        public async Task<Station> GetStationById(int id)
        {
            try
            {
                return await db.Stations.SingleOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Station>> GetStations()
        {
            return await db.Stations.ToListAsync();
        }

        public async Task<Station> UpdateStation(Station station)
        {
            var oldStation = await GetStationById(station.Id);
            if (oldStation != null)
            {
                var userType = typeof(Station);
                foreach (var property in userType.GetProperties())
                {
                    var newValue = property.GetValue(station);
                    if (newValue != null && !string.IsNullOrWhiteSpace(newValue.ToString()))
                    {
                        property.SetValue(oldStation, newValue);
                    }
                }
                db.Stations.Update(oldStation);
                await db.SaveChangesAsync();
                return oldStation;
            }
            else
            {
                throw new ArgumentException("No ID found");
            }
        }
    }
}
