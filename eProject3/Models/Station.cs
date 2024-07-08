namespace eProject3.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string Station_name { get; set; }
        public string Station_code { get; set; }
        public string Division_name { get; set; }
        public int distance { get; set; }
        public int? ReservationID { get; set; }

        public virtual Reservation? Reservations { get; set; }
    }
}
