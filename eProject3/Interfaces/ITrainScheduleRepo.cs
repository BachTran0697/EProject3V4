using eProject3.Models;

namespace eProject3.Interfaces
{
    public interface ITrainScheduleRepo
    {
        Task<IEnumerable<Train_Schedule>> GetSchedule();
        Task<Train_Schedule> GetScheduleById(int id);
        Task<Train_Schedule> CreateSchedule(Train_Schedule train_Schedule);
        Task<Train_Schedule> UpdateSchedule(Train_Schedule train_Schedule);
        Task<Train_Schedule> DeleteSchedule(int id);
        Task<List<Train_Schedule>> DayMaster();
        Task<List<Train_Schedule>> Booking(int fromStation,int toStation, DateTime travelTime);

    }
}
