using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eProject3.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cancellations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CancellationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CancellationFee = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cancellations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CancelRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket_Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "confirmCancelRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CancellationFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_confirmCancelRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price_on_type = table.Column<int>(type: "int", nullable: false),
                    BaseFarePerKm = table.Column<int>(type: "int", nullable: false),
                    AdditionalCharges = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    TrainType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Speed = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    LOGIN_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOGIN_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LOGIN_PASSWORD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    delete = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.LOGIN_ID);
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainId = table.Column<int>(type: "int", nullable: true),
                    CoachNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatsNumber = table.Column<int>(type: "int", nullable: false),
                    Seats_vacant = table.Column<int>(type: "int", nullable: true),
                    Seats_reserved = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coaches_Trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Trains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ticket_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Station_begin_id = table.Column<int>(type: "int", nullable: false),
                    Station_end_id = table.Column<int>(type: "int", nullable: false),
                    Time_begin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time_end = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Train_id = table.Column<int>(type: "int", nullable: false),
                    Coach_id = table.Column<int>(type: "int", nullable: false),
                    Seat_id = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    CancellationFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CancelledDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Trains_Train_id",
                        column: x => x.Train_id,
                        principalTable: "Trains",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Train_Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainId = table.Column<int>(type: "int", nullable: false),
                    Route = table.Column<int>(type: "int", nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Station_Code_begin = table.Column<int>(type: "int", nullable: false),
                    Station_code_end = table.Column<int>(type: "int", nullable: false),
                    Station_code_pass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time_begin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time_end = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DetailID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Train_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Train_Schedules_Trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Trains",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoachId = table.Column<int>(type: "int", nullable: true),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CancelResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationDetailsId = table.Column<int>(type: "int", nullable: false),
                    CancellationFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancelResponses_Reservations_ReservationDetailsId",
                        column: x => x.ReservationDetailsId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Station_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Station_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Division_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    distance = table.Column<int>(type: "int", nullable: false),
                    ReservationID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stations_Reservations_ReservationID",
                        column: x => x.ReservationID,
                        principalTable: "Reservations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Train_Schedule_Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Train_ScheduleId = table.Column<int>(type: "int", nullable: false),
                    Station_Code_begin = table.Column<int>(type: "int", nullable: false),
                    Station_code_end = table.Column<int>(type: "int", nullable: false),
                    Seat_reserved = table.Column<int>(type: "int", nullable: false),
                    Seat_vacant = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Train_Schedule_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Train_Schedule_Details_Train_Schedules_Train_ScheduleId",
                        column: x => x.Train_ScheduleId,
                        principalTable: "Train_Schedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SeatDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatId = table.Column<int>(type: "int", nullable: true),
                    Station_code_begin = table.Column<int>(type: "int", nullable: false),
                    Station_code_end = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatDetails_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Fares",
                columns: new[] { "Id", "AdditionalCharges", "BaseFarePerKm", "ClassType", "Price_on_type" },
                values: new object[,]
                {
                    { 1, 50, 10, "First Class", 500 },
                    { 2, 40, 8, "Second Class", 400 },
                    { 3, 30, 7, "Sleeper Class", 300 }
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "Division_name", "ReservationID", "Station_code", "Station_name", "distance" },
                values: new object[,]
                {
                    { 1, "nam", null, "HCM", "SaiGon", 0 },
                    { 2, "trung", null, "DN", "DaNang", 0 },
                    { 3, "bac", null, "HN", "NoiBai", 0 },
                    { 4, "bac", null, "CB", "CaoBang", 0 }
                });

            migrationBuilder.InsertData(
                table: "Trains",
                columns: new[] { "Id", "RouteId", "Speed", "TrainName", "TrainNo", "TrainType" },
                values: new object[,]
                {
                    { 1, 1, "120km/h", "Express", "T123", "Electric" },
                    { 2, 2, "90km/h", "Local", "T124", "Diesel" },
                    { 3, 3, "110km/h", "Regional", "T125", "Hybrid" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "LOGIN_ID", "Address", "Email", "LOGIN_NAME", "LOGIN_PASSWORD", "delete", "role_id", "status" },
                values: new object[,]
                {
                    { 1, "AAA", "aaa@gmail.com", "admin", "123", null, 1, null },
                    { 2, "BBB", "bbb@gmail.com", "account", "123", null, 2, null }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CancellationFee", "CancelledDate", "Coach_id", "Email", "IsCancelled", "Name", "Phone", "Price", "Seat_id", "Station_begin_id", "Station_end_id", "Ticket_code", "Time_begin", "Time_end", "Train_id" },
                values: new object[] { 1, null, null, 1, "abc", false, "abc", "123", 100m, 1, 1, 3, "ticket001", new DateTime(2024, 7, 11, 1, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 12, 1, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Train_Schedules",
                columns: new[] { "Id", "DetailID", "Direction", "Route", "Station_Code_begin", "Station_code_end", "Station_code_pass", "Time_begin", "Time_end", "TrainId" },
                values: new object[,]
                {
                    { 1, null, "down", 2, 1, 3, "2", new DateTime(2024, 7, 5, 1, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 7, 1, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, null, "down", 2, 2, 4, "3", new DateTime(2024, 7, 5, 1, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 8, 1, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, null, "down", 2, 1, 4, "2,3", new DateTime(2024, 7, 6, 1, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 9, 1, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CancelResponses_ReservationDetailsId",
                table: "CancelResponses",
                column: "ReservationDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_TrainId",
                table: "Coaches",
                column: "TrainId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Train_id",
                table: "Reservations",
                column: "Train_id");

            migrationBuilder.CreateIndex(
                name: "IX_SeatDetails_SeatId",
                table: "SeatDetails",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_CoachId",
                table: "Seats",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_ReservationID",
                table: "Stations",
                column: "ReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_Train_Schedule_Details_Train_ScheduleId",
                table: "Train_Schedule_Details",
                column: "Train_ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Train_Schedules_TrainId",
                table: "Train_Schedules",
                column: "TrainId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cancellations");

            migrationBuilder.DropTable(
                name: "CancelRequests");

            migrationBuilder.DropTable(
                name: "CancelResponses");

            migrationBuilder.DropTable(
                name: "confirmCancelRequests");

            migrationBuilder.DropTable(
                name: "Fares");

            migrationBuilder.DropTable(
                name: "SeatDetails");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Train_Schedule_Details");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Train_Schedules");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "Trains");
        }
    }
}
