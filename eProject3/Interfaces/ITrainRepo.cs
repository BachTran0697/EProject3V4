using eProject3.Models;

namespace eProject3.Interfaces
{
    public interface ITrainRepo
    {
        Task<IEnumerable<Train>> GetTrains();
        Task<Train> GetTrainById(int id);
        Task<Train> CreateTrain(Train train);
        Task<Train> UpdateTrain(Train train);
        Task<Train> DeleteTrain(int id);
    }
}
