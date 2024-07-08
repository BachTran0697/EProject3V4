using System.ComponentModel.DataAnnotations.Schema;

namespace eProject3.Models
{
    public class Train_Schedule_Detail
    {
        public int Id { get; set; }
        public int Train_ScheduleId { get; set; }
        public int Station_Code_begin { get; set; }
        public int Station_code_end { get; set; }
        public int Seat_reserved { get; set; }
        public int Seat_vacant { get; set; }

        [ForeignKey("Train_ScheduleId")]
        public virtual Train_Schedule? Train_Schedule { get; set; }
    }
}

