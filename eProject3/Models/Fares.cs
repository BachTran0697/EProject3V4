using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject3.Models
{
    public class Fares
    {
        [Key]
        public int Id { get; set; }
        public string ClassType { get; set; }
        public int Price_on_type { get; set; }
        public int BaseFarePerKm { get; set; }
        public int AdditionalCharges { get; set; }

        
    }
}
