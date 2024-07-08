using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace eProject3.Models
{
    public class Coach
    {
        [Key]
        public int Id { get; set; }
        public int? TrainId { get; set; }
        public string CoachNumber { get; set; }
        public string ClassType { get; set; }
        public int SeatsNumber { get; set; }
        public int? Seats_vacant { get; set; }
        public int? Seats_reserved { get; set; }

        [JsonIgnore]
        public virtual Train? Train { get; set; }
        [JsonIgnore]
        public virtual ICollection<Seat>? Seats { get; set; }
        
    }
}
