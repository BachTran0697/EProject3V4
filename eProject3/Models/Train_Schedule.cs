using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eProject3.Models
{
    public class Train_Schedule
    {
        [Key]
        public int Id { get; set; }
        public int TrainId { get; set; }
        public int Route { get; set; }
        public string Direction { get; set; }
        public int Station_Code_begin { get; set; }
        public int Station_code_end { get; set; }
        public string? Station_code_pass { get; set; }
        public DateTime Time_begin { get; set; }
        public DateTime Time_end { get; set; }
        public int? DetailID { get; set; }
        public virtual ICollection<Train_Schedule_Detail>? Detail { get; set; }
        public virtual Train? Train { get; set; }
    }
}
