using eProject3.Controllers;
using Microsoft.EntityFrameworkCore;

namespace eProject3.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Fares> Fares { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        
        public DbSet<Train_Schedule> Train_Schedules { get; set; }
        public DbSet<Train_Schedule_Detail> Train_Schedule_Details { get; set; }
        public DbSet<SeatDetail> SeatDetails { get; set; }
        public DbSet<CancelRequest> CancelRequests { get; set; }
        public DbSet<CancelResponse> CancelResponses { get; set; }
        public DbSet<ConfirmCancelRequest> confirmCancelRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entity configurations and relationships
            modelBuilder.Entity<Coach>()
                .HasOne(c => c.Train)
                .WithMany(t => t.Coaches)
                .HasForeignKey(c => c.TrainId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Coach)
                .WithMany(c => c.Seats)
                .HasForeignKey(s => s.CoachId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<SeatDetail>()
                .HasOne(s => s.Seat)
                .WithMany(sd => sd.SeatDetails)
                .HasForeignKey(s => s.SeatId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Train_Schedule_Detail>()
                .HasOne(tsd => tsd.Train_Schedule)
                .WithMany(ts => ts.Detail)
                .HasForeignKey(tsd => tsd.Train_ScheduleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Station>()
                .HasOne(s => s.Reservations)
                .WithMany(r => r.Station)
                .HasForeignKey(s => s.ReservationID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Train)
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.Train_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Train_Schedule>()
                .HasOne(ts => ts.Train)
                .WithMany(t => t.TrainSchedules)
                .HasForeignKey(ts => ts.TrainId)
                .OnDelete(DeleteBehavior.NoAction);

            // Seed data
            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User {LOGIN_ID = 1, LOGIN_NAME = "admin", Address = "AAA", Email = "aaa@gmail.com", LOGIN_PASSWORD = "123", role_id = 1},
                new User {LOGIN_ID = 2, LOGIN_NAME = "account", Address = "BBB", Email = "bbb@gmail.com", LOGIN_PASSWORD = "123", role_id = 2},
                new User {LOGIN_ID = 3, LOGIN_NAME = "manager", Address = "CCC", Email = "ccc@gmail.com", LOGIN_PASSWORD = "123", role_id = 3}
            });

            modelBuilder.Entity<Station>().HasData(new Station[]
            {
                new Station { Id = 1, Station_name = "HaNoi", Station_code = "HN", Division_name = "bac", distance=0 },
                new Station { Id = 2, Station_name = "NinhBinh", Station_code = "NB", Division_name = "bac", distance=115 },
                new Station { Id = 3, Station_name = "Vinh", Station_code = "V", Division_name = "bac", distance = 319 },
                new Station { Id = 4, Station_name = "DongHoi", Station_code = "DH", Division_name = "bac", distance = 522 },
                new Station { Id = 5, Station_name = "Hue", Station_code = "H", Division_name = "trung", distance = 688 },
                new Station { Id = 6, Station_name = "ThanhKhe", Station_code = "TK", Division_name = "trung", distance = 788 },
                new Station { Id = 7, Station_name = "DaNang", Station_code = "DN", Division_name = "trung", distance = 791 },
                new Station { Id = 8, Station_name = "QuangNgai", Station_code = "QNG", Division_name = "trung", distance = 928 },
                new Station { Id = 9, Station_name = "QuyNhon", Station_code = "QNH", Division_name = "trung", distance = 950 },
                new Station { Id = 10, Station_name = "NhaTrang", Station_code = "NT", Division_name = "trung", distance = 1315 },
                new Station { Id = 11, Station_name = "PhanThiet", Station_code = "PT", Division_name = "nam", distance = 1500 },
                new Station { Id = 12, Station_name = "BienHoa", Station_code = "BH", Division_name = "nam", distance = 1697 },
                new Station { Id = 13, Station_name = "SaiGon", Station_code = "SG", Division_name = "nam", distance = 1726 }
            });



            modelBuilder.Entity<Train>().HasData(new Train[]
            {
                new Train { Id = 1, TrainNo = "T001", TrainName = "Express", RouteId = 1, TrainType = "Electric", Speed = "200" },
                new Train { Id = 2, TrainNo = "T002", TrainName = "Local", RouteId = 2, TrainType = "Diesel", Speed = "250" },
                new Train { Id = 3, TrainNo = "T003", TrainName = "Regional", RouteId = 3, TrainType = "Hybrid", Speed = "300" },
                new Train { Id = 4, TrainNo = "T004", TrainName = "SuperSpeed", RouteId = 4, TrainType = "Electric", Speed = "500" },
                new Train { Id = 5, TrainNo = "T005", TrainName = "SightSeeing", RouteId = 5, TrainType = "Hybrid", Speed = "150" }
            });

            modelBuilder.Entity<Fares>().HasData(new Fares[]
            {
                new Fares { Id = 1, ClassType = "First Class", Price_on_type = 500, BaseFarePerKm = 10, AdditionalCharges = 50 },
                new Fares { Id = 2, ClassType = "Second Class", Price_on_type = 400, BaseFarePerKm = 8, AdditionalCharges = 40 },
                new Fares { Id = 3, ClassType = "Sleeper Class", Price_on_type = 1000, BaseFarePerKm = 17, AdditionalCharges = 30 }
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
