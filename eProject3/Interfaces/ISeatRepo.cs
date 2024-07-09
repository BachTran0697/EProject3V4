using eProject3.Models;

namespace eProject3.Interfaces
{
    public interface ISeatRepo
    {
        Task<IEnumerable<Seat>> GetSeats();
        Task<IEnumerable<Seat>> GetSeatsByCoachId(int coachId);
        Task<Seat> CreateSeat(Seat seat);
        Task<Seat> UpdateSeat(Seat seat);
        Task<Seat> DeleteSeat(int id);
    }
}
