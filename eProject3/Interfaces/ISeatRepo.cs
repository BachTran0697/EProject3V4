using eProject3.Models;

namespace eProject3.Interfaces
{
    public interface ISeatRepo
    {
        Task<IEnumerable<Seat>> GetSeats();
        Task<Seat> GetSeatById(int id);
        Task<Seat> CreateSeat(Seat seat);
        Task<Seat> UpdateSeat(Seat seat);
        Task<Seat> DeleteSeat(int id);
        Task<IEnumerable<Seat>> GetSeatsByCoachId(int coachId);
    }
}
