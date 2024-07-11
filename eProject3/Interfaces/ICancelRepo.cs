using eProject3.Models;

namespace eProject3.Interfaces
{
    public interface ICancelRepo
    {

        Task<CancelResponse> CancelReservationAsync(string ticketcode, string email, string phone);
        Task<IEnumerable<Reservation>> GetCancellations();
    }
}
