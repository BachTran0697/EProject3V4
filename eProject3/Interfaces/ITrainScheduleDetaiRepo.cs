using eProject3.Models;

namespace eProject3.Interfaces
{
    public interface ITrainScheduleDetaiRepo
    {
        Task<IEnumerable<Train_Schedule_Detail>> GetScheduleDetail();
        Task<Train_Schedule_Detail> GetScheduleDetailById(int id);
    }
}
