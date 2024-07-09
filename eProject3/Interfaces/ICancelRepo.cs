using eProject3.Models;

namespace eProject3.Interfaces
{
    public interface ICancelRepo
    {
        Task<IEnumerable<Cancellation>> GetCancellations();
        Task<Cancellation> GetCancellationById(int id);
        Task<Cancellation> CreateCancellation(Cancellation Cancellation);
        Task<Cancellation> UpdateCancellation(Cancellation Cancellation);
        Task<Cancellation> DeleteCancellation(int id);
        Task<CancelResponse> CancelReservationAsync(CancelRequest request);
        Task<string> ConfirmCancelAsync(ConfirmCancelRequest request);
    }
}
