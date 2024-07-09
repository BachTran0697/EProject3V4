using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject3.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Ticket_code { get; set; } //để null vì xử lý trong backend sau khi khách đặt vé
        public int Station_begin_id { get; set; }
        public int Station_end_id { get; set; }
        public DateTime Time_begin { get; set; }
        public DateTime Time_end { get; set; }
        public int Train_id { get; set; }
        public int Coach_id { get; set; }
        public int Seat_id { get; set; }
        public decimal? Price { get; set; } 
        public bool IsCancelled { get; set; }
        public decimal? CancellationFee { get; set; }
        public DateTime? CancelledDate { get; set; }
        public virtual Train? Train { get; set; }
        public virtual ICollection<Station>? Station { get; set; }
    }
}
