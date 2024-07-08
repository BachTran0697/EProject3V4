using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace eProject3.Models
{
    public class Train
    {
        [Key]
        public int Id { get; set; }
        public string TrainNo { get; set; }
        public string TrainName { get; set; }
        public int RouteId { get; set; }
        public string TrainType { get; set; }
        public string Speed { get; set; }

        [JsonIgnore]
        public virtual ICollection<Coach>? Coaches { get; set; }
        public virtual ICollection<Reservation>? Reservations { get; set; }
        public virtual ICollection<Train_Schedule>? TrainSchedules { get; set;}
        
    }

}
