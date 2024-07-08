using eProject3.Interfaces;
using eProject3.Models;

namespace eProject3.Repositories
{
    public class TrainScheduleDetailRepo : ITrainScheduleDetaiRepo
    {
        public Task<IEnumerable<Train_Schedule_Detail>> GetScheduleDetail()
        {
            throw new NotImplementedException();
        }

        public Task<Train_Schedule_Detail> GetScheduleDetailById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
