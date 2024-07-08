using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject3.Models
{
    public class Cancellation
    {
        [Key]
        public int Id { get; set; }
        public string Ticket_code { get; set; }
        public DateTime CancellationDate { get; set; }
        public float CancellationFee { get; set; }


    }
}
