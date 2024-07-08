﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eProject3.Models;

#nullable disable

namespace eProject3.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("eProject3.Models.Cancellation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CancellationDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("CancellationFee")
                        .HasColumnType("real");

                    b.Property<string>("Ticket_code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cancellations");
                });

            modelBuilder.Entity("eProject3.Models.Coach", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClassType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoachNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SeatsNumber")
                        .HasColumnType("int");

                    b.Property<int?>("Seats_reserved")
                        .HasColumnType("int");

                    b.Property<int?>("Seats_vacant")
                        .HasColumnType("int");

                    b.Property<int?>("TrainId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TrainId");

                    b.ToTable("Coaches");
                });

            modelBuilder.Entity("eProject3.Models.Fares", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdditionalCharges")
                        .HasColumnType("int");

                    b.Property<int>("BaseFarePerKm")
                        .HasColumnType("int");

                    b.Property<string>("ClassType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price_on_type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Fares");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AdditionalCharges = 50,
                            BaseFarePerKm = 10,
                            ClassType = "First Class",
                            Price_on_type = 500
                        },
                        new
                        {
                            Id = 2,
                            AdditionalCharges = 40,
                            BaseFarePerKm = 8,
                            ClassType = "Second Class",
                            Price_on_type = 400
                        },
                        new
                        {
                            Id = 3,
                            AdditionalCharges = 30,
                            BaseFarePerKm = 7,
                            ClassType = "Sleeper Class",
                            Price_on_type = 300
                        });
                });

            modelBuilder.Entity("eProject3.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Coach_id")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Seat_id")
                        .HasColumnType("int");

                    b.Property<int>("Station_begin_id")
                        .HasColumnType("int");

                    b.Property<int>("Station_end_id")
                        .HasColumnType("int");

                    b.Property<string>("Ticket_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time_begin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Time_end")
                        .HasColumnType("datetime2");

                    b.Property<int>("Train_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Train_id");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("eProject3.Models.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CoachId")
                        .HasColumnType("int");

                    b.Property<string>("SeatNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("eProject3.Models.SeatDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("SeatId")
                        .HasColumnType("int");

                    b.Property<int>("Station_code_begin")
                        .HasColumnType("int");

                    b.Property<int>("Station_code_end")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SeatId");

                    b.ToTable("SeatDetails");
                });

            modelBuilder.Entity("eProject3.Models.Station", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Division_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReservationID")
                        .HasColumnType("int");

                    b.Property<string>("Station_code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Station_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("distance")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReservationID");

                    b.ToTable("Stations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Division_name = "nam",
                            Station_code = "HCM",
                            Station_name = "SaiGon",
                            distance = 0
                        },
                        new
                        {
                            Id = 2,
                            Division_name = "trung",
                            Station_code = "DN",
                            Station_name = "DaNang",
                            distance = 0
                        },
                        new
                        {
                            Id = 3,
                            Division_name = "bac",
                            Station_code = "HN",
                            Station_name = "NoiBai",
                            distance = 0
                        },
                        new
                        {
                            Id = 4,
                            Division_name = "bac",
                            Station_code = "CB",
                            Station_name = "CaoBang",
                            distance = 0
                        });
                });

            modelBuilder.Entity("eProject3.Models.Train", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.Property<string>("Speed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrainName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrainNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrainType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Trains");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RouteId = 1,
                            Speed = "120km/h",
                            TrainName = "Express",
                            TrainNo = "T123",
                            TrainType = "Electric"
                        },
                        new
                        {
                            Id = 2,
                            RouteId = 2,
                            Speed = "90km/h",
                            TrainName = "Local",
                            TrainNo = "T124",
                            TrainType = "Diesel"
                        },
                        new
                        {
                            Id = 3,
                            RouteId = 3,
                            Speed = "110km/h",
                            TrainName = "Regional",
                            TrainNo = "T125",
                            TrainType = "Hybrid"
                        });
                });

            modelBuilder.Entity("eProject3.Models.Train_Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DetailID")
                        .HasColumnType("int");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Route")
                        .HasColumnType("int");

                    b.Property<int>("Station_Code_begin")
                        .HasColumnType("int");

                    b.Property<int>("Station_code_end")
                        .HasColumnType("int");

                    b.Property<string>("Station_code_pass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time_begin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Time_end")
                        .HasColumnType("datetime2");

                    b.Property<int>("TrainId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TrainId");

                    b.ToTable("Train_Schedules");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Direction = "down",
                            Route = 2,
                            Station_Code_begin = 1,
                            Station_code_end = 3,
                            Station_code_pass = "2",
                            Time_begin = new DateTime(2024, 7, 5, 1, 0, 0, 0, DateTimeKind.Unspecified),
                            Time_end = new DateTime(2024, 7, 7, 1, 0, 0, 0, DateTimeKind.Unspecified),
                            TrainId = 1
                        },
                        new
                        {
                            Id = 2,
                            Direction = "down",
                            Route = 2,
                            Station_Code_begin = 2,
                            Station_code_end = 4,
                            Station_code_pass = "3",
                            Time_begin = new DateTime(2024, 7, 5, 1, 0, 0, 0, DateTimeKind.Unspecified),
                            Time_end = new DateTime(2024, 7, 8, 1, 0, 0, 0, DateTimeKind.Unspecified),
                            TrainId = 1
                        },
                        new
                        {
                            Id = 3,
                            Direction = "down",
                            Route = 2,
                            Station_Code_begin = 1,
                            Station_code_end = 4,
                            Station_code_pass = "2,3",
                            Time_begin = new DateTime(2024, 7, 6, 1, 0, 0, 0, DateTimeKind.Unspecified),
                            Time_end = new DateTime(2024, 7, 9, 1, 0, 0, 0, DateTimeKind.Unspecified),
                            TrainId = 1
                        });
                });

            modelBuilder.Entity("eProject3.Models.Train_Schedule_Detail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Seat_reserved")
                        .HasColumnType("int");

                    b.Property<int>("Seat_vacant")
                        .HasColumnType("int");

                    b.Property<int>("Station_Code_begin")
                        .HasColumnType("int");

                    b.Property<int>("Station_code_end")
                        .HasColumnType("int");

                    b.Property<int>("Train_ScheduleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Train_ScheduleId");

                    b.ToTable("Train_Schedule_Details");
                });

            modelBuilder.Entity("eProject3.Models.User", b =>
                {
                    b.Property<int>("LOGIN_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LOGIN_ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LOGIN_NAME")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LOGIN_PASSWORD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("delete")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("role_id")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LOGIN_ID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            LOGIN_ID = 1,
                            Address = "AAA",
                            Email = "aaa@gmail.com",
                            LOGIN_NAME = "admin",
                            LOGIN_PASSWORD = "123",
                            role_id = 1
                        },
                        new
                        {
                            LOGIN_ID = 2,
                            Address = "BBB",
                            Email = "bbb@gmail.com",
                            LOGIN_NAME = "account",
                            LOGIN_PASSWORD = "123",
                            role_id = 2
                        });
                });

            modelBuilder.Entity("eProject3.Models.Coach", b =>
                {
                    b.HasOne("eProject3.Models.Train", "Train")
                        .WithMany("Coaches")
                        .HasForeignKey("TrainId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Train");
                });

            modelBuilder.Entity("eProject3.Models.Reservation", b =>
                {
                    b.HasOne("eProject3.Models.Train", "Train")
                        .WithMany("Reservations")
                        .HasForeignKey("Train_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Train");
                });

            modelBuilder.Entity("eProject3.Models.Seat", b =>
                {
                    b.HasOne("eProject3.Models.Coach", "Coach")
                        .WithMany("Seats")
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("eProject3.Models.SeatDetail", b =>
                {
                    b.HasOne("eProject3.Models.Seat", "Seat")
                        .WithMany("SeatDetails")
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Seat");
                });

            modelBuilder.Entity("eProject3.Models.Station", b =>
                {
                    b.HasOne("eProject3.Models.Reservation", "Reservations")
                        .WithMany("Station")
                        .HasForeignKey("ReservationID")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("eProject3.Models.Train_Schedule", b =>
                {
                    b.HasOne("eProject3.Models.Train", "Train")
                        .WithMany("TrainSchedules")
                        .HasForeignKey("TrainId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Train");
                });

            modelBuilder.Entity("eProject3.Models.Train_Schedule_Detail", b =>
                {
                    b.HasOne("eProject3.Models.Train_Schedule", "Train_Schedule")
                        .WithMany("Detail")
                        .HasForeignKey("Train_ScheduleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Train_Schedule");
                });

            modelBuilder.Entity("eProject3.Models.Coach", b =>
                {
                    b.Navigation("Seats");
                });

            modelBuilder.Entity("eProject3.Models.Reservation", b =>
                {
                    b.Navigation("Station");
                });

            modelBuilder.Entity("eProject3.Models.Seat", b =>
                {
                    b.Navigation("SeatDetails");
                });

            modelBuilder.Entity("eProject3.Models.Train", b =>
                {
                    b.Navigation("Coaches");

                    b.Navigation("Reservations");

                    b.Navigation("TrainSchedules");
                });

            modelBuilder.Entity("eProject3.Models.Train_Schedule", b =>
                {
                    b.Navigation("Detail");
                });
#pragma warning restore 612, 618
        }
    }
}
