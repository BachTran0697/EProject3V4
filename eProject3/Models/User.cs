using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject3.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LOGIN_ID { get; set; }
        public string LOGIN_NAME { get; set; }
        public string Email { get; set; }
        public string LOGIN_PASSWORD { get; set; }
        public int role_id { get; set; }
        public string Address { get; set; }
        public string? delete { get; set; }
        public string? status { get; set; }
    }
}
