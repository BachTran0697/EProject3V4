using eProject3.Models;

namespace eProject3.Interfaces
{
    public interface IReservedRepo
    {
        Task<IEnumerable<Reservation>> GetReservations();
        Task<Reservation> GetReservationById(int id);
        Task<Reservation> CreateReservation(Reservation Reservation);
        Task<Reservation> UpdateReservation(Reservation Reservation);
        Task<List<Reservation>> FinishReservation();
        Task<Reservation> GetByPRNAsync(string prn);
        Task UpdateAsync(Reservation reservation);
        Task<IEnumerable<Reservation>> GetReservationsByUserAsync(string name, string phone);
        Task<Reservation> PayCheck(int id);
        Task<(DateTime timeBegin, DateTime timeEnd)> ConfirmReserved(Reservation reservation);
    }
}
