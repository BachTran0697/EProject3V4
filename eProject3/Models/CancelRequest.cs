using System.ComponentModel.DataAnnotations;

namespace eProject3.Models
{
    public class CancelRequest
    {
        [Key]
        public int Id { get; set; }
        public string Ticket_Code { get; set; }
    }
}
