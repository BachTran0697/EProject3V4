using eProject3.Models;

namespace eProject3.Interfaces
{
    public interface ICoachRepo
    {
        Task<IEnumerable<Coach>> GetCoachs();
        Task<Coach> GetCoachById(int id);
        Task<Coach> CreateCoach(Coach coach);
        Task<Coach> UpdateCoach(Coach coach);
        Task<Coach> DeleteCoach(int id);
        Task<IEnumerable<Coach>> GetCoachesByTrainId(int trainId);
    }
}
