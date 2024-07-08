using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject3.Models
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }
        public int? CoachId { get; set; }
        public string SeatNumber { get; set; }
        public virtual Coach? Coach { get; set; }
        public virtual ICollection<SeatDetail>? SeatDetails { get; set; }

    }
}
