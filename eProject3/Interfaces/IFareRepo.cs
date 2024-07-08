using eProject3.Models;

namespace eProject3.Interfaces
{
    public interface IFaresRepo
    {
        Task<IEnumerable<Fares>> GetFares();
        Task<Fares> GetFareById(int id);
        Task<Fares> CreateFare(Fares fare);
        Task<Fares> UpdateFare(Fares fare);
        Task<Fares> DeleteFare(int id);
    }
}
