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
        public DbSet<Cancellation> Cancellations { get; set; }
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
                new User {LOGIN_ID = 2, LOGIN_NAME = "account", Address = "BBB", Email = "bbb@gmail.com", LOGIN_PASSWORD = "123", role_id = 2}
            });

            modelBuilder.Entity<Station>().HasData(new Station[]
            {
                new Station { Id = 1, Station_name = "SaiGon", Station_code = "HCM", Division_name = "nam" },
                new Station { Id = 2, Station_name = "DaNang", Station_code = "DN", Division_name = "trung" },
                new Station { Id = 3, Station_name = "NoiBai", Station_code = "HN", Division_name = "bac" },
                new Station { Id = 4, Station_name = "CaoBang", Station_code = "CB", Division_name = "bac" }
            });



            modelBuilder.Entity<Train>().HasData(new Train[]
            {
                new Train { Id = 1, TrainNo = "T123", TrainName = "Express", RouteId = 1, TrainType = "Electric", Speed = "120km/h" },
                new Train { Id = 2, TrainNo = "T124", TrainName = "Local", RouteId = 2, TrainType = "Diesel", Speed = "90km/h" },
                new Train { Id = 3, TrainNo = "T125", TrainName = "Regional", RouteId = 3, TrainType = "Hybrid", Speed = "110km/h" }
            });

            modelBuilder.Entity<Train_Schedule>().HasData(new Train_Schedule[]
            {
                new Train_Schedule { Id = 1, TrainId = 1, Station_Code_begin = 1, Station_code_end = 3, Station_code_pass = "2", Direction="down", Route=2, Time_begin=new DateTime(2024,7,5,01,00,00), Time_end=new DateTime(2024,7,7,1,00,00) },
                new Train_Schedule { Id = 2, TrainId = 1, Station_Code_begin = 2, Station_code_end = 4, Station_code_pass = "3", Direction="down", Route=2, Time_begin=new DateTime(2024,7,5,01,00,00), Time_end=new DateTime(2024,7,8,1,00,00) },
                new Train_Schedule { Id = 3, TrainId = 1, Station_Code_begin = 1, Station_code_end = 4, Station_code_pass = "2,3", Direction="down", Route=2, Time_begin=new DateTime(2024,7,6,01,00,00), Time_end=new DateTime(2024,7,9,1,00,00)  }
            });



            modelBuilder.Entity<Fares>().HasData(new Fares[]
            {
                new Fares { Id = 1, ClassType = "First Class", Price_on_type = 500, BaseFarePerKm = 10, AdditionalCharges = 50 },
                new Fares { Id = 2, ClassType = "Second Class", Price_on_type = 400, BaseFarePerKm = 8, AdditionalCharges = 40 },
                new Fares { Id = 3, ClassType = "Sleeper Class", Price_on_type = 300, BaseFarePerKm = 7, AdditionalCharges = 30 }
            });

            modelBuilder.Entity<Reservation>().HasData(new Reservation[]
            {
                new Reservation { Id = 1, Name="abc", Email="abc", Phone="123", Station_begin_id = 1, Station_end_id =3,
                                  Time_begin=new DateTime(2024,7,11,01,00,00), Time_end = new DateTime(2024,7,12,01,00,00),
                                  Ticket_code = "ticket001", Train_id=1,Coach_id=1,Seat_id=1, Price=100m, IsCancelled=false}
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
