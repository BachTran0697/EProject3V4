using System.ComponentModel.DataAnnotations;


namespace eProject3.Models
{
    public class SeatDetail
    {
        [Key]
        public int Id { get; set; }
        public int? SeatId { get; set; }
        public int Station_code_begin { get; set; }
        public int Station_code_end { get; set; }
        public string Status { get; set; }
        public virtual Seat? Seat { get; set; }

    }
}
