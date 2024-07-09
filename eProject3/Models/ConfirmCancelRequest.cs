using System.ComponentModel.DataAnnotations;

namespace eProject3.Models
{
    public class ConfirmCancelRequest
    {
        [Key]
        public int Id { get; set; }
        public string Ticket_Code { get; set; }
        public decimal CancellationFee { get; set; }
    }
}
