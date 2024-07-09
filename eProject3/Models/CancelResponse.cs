using System.ComponentModel.DataAnnotations;

namespace eProject3.Models
{
    public class CancelResponse
    {
        [Key]
        public int Id { get; set; }
        public Reservation ReservationDetails { get; set; }
        public decimal CancellationFee { get; set; }
    }
}
