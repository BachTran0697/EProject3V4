using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eProject3.Migrations
{
    /// <inheritdoc />
    public partial class _12071521 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Train_Schedules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Train_Schedules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Train_Schedules",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Fares",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BaseFarePerKm", "Price_on_type" },
                values: new object[] { 17, 1000 });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Division_name", "Station_code", "Station_name" },
                values: new object[] { "bac", "HN", "HaNoi" });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Division_name", "Station_code", "Station_name", "distance" },
                values: new object[] { "bac", "NB", "NinhBinh", 115 });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Station_code", "Station_name", "distance" },
                values: new object[] { "V", "Vinh", 319 });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Station_code", "Station_name", "distance" },
                values: new object[] { "DH", "DongHoi", 522 });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "Division_name", "ReservationID", "Station_code", "Station_name", "distance" },
                values: new object[,]
                {
                    { 5, "trung", null, "H", "Hue", 688 },
                    { 6, "trung", null, "TK", "ThanhKhe", 788 },
                    { 7, "trung", null, "DN", "DaNang", 791 },
                    { 8, "trung", null, "QNG", "QuangNgai", 928 },
                    { 9, "trung", null, "QNH", "QuyNhon", 950 },
                    { 10, "trung", null, "NT", "NhaTrang", 1315 },
                    { 11, "nam", null, "PT", "PhanThiet", 1500 },
                    { 12, "nam", null, "BH", "BienHoa", 1697 },
                    { 13, "nam", null, "SG", "SaiGon", 1726 }
                });

            migrationBuilder.UpdateData(
                table: "Trains",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Speed", "TrainNo" },
                values: new object[] { "200", "T001" });

            migrationBuilder.UpdateData(
                table: "Trains",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Speed", "TrainNo" },
                values: new object[] { "250", "T002" });

            migrationBuilder.UpdateData(
                table: "Trains",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Speed", "TrainNo" },
                values: new object[] { "300", "T003" });

            migrationBuilder.InsertData(
                table: "Trains",
                columns: new[] { "Id", "RouteId", "Speed", "TrainName", "TrainNo", "TrainType" },
                values: new object[,]
                {
                    { 4, 4, "500", "SuperSpeed", "T004", "Electric" },
                    { 5, 5, "150", "SightSeeing", "T005", "Hybrid" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "LOGIN_ID", "Address", "Email", "LOGIN_NAME", "LOGIN_PASSWORD", "delete", "role_id", "status" },
                values: new object[] { 3, "CCC", "ccc@gmail.com", "manager", "123", null, 3, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Trains",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Trains",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "LOGIN_ID",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Fares",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BaseFarePerKm", "Price_on_type" },
                values: new object[] { 7, 300 });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CancellationFee", "CancelledDate", "Coach_id", "Email", "IsCancelled", "Name", "PayStatus", "Phone", "Price", "Seat_id", "Station_begin_id", "Station_end_id", "Ticket_code", "Time_begin", "Time_end", "Train_id" },
                values: new object[] { 1, null, null, 1, "abc", false, "abc", null, "123", 100m, 1, 1, 3, "ticket001", new DateTime(2024, 7, 11, 1, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 12, 1, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Division_name", "Station_code", "Station_name" },
                values: new object[] { "nam", "HCM", "SaiGon" });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Division_name", "Station_code", "Station_name", "distance" },
                values: new object[] { "trung", "DN", "DaNang", 0 });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Station_code", "Station_name", "distance" },
                values: new object[] { "HN", "NoiBai", 0 });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Station_code", "Station_name", "distance" },
                values: new object[] { "CB", "CaoBang", 0 });

            migrationBuilder.InsertData(
                table: "Train_Schedules",
                columns: new[] { "Id", "DetailID", "Direction", "Route", "Station_Code_begin", "Station_code_end", "Station_code_pass", "Time_begin", "Time_end", "TrainId" },
                values: new object[,]
                {
                    { 1, null, "down", 2, 1, 3, "2", new DateTime(2024, 7, 5, 1, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 7, 1, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, null, "down", 2, 2, 4, "3", new DateTime(2024, 7, 5, 1, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 8, 1, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, null, "down", 2, 1, 4, "2,3", new DateTime(2024, 7, 6, 1, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 9, 1, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.UpdateData(
                table: "Trains",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Speed", "TrainNo" },
                values: new object[] { "120km/h", "T123" });

            migrationBuilder.UpdateData(
                table: "Trains",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Speed", "TrainNo" },
                values: new object[] { "90km/h", "T124" });

            migrationBuilder.UpdateData(
                table: "Trains",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Speed", "TrainNo" },
                values: new object[] { "110km/h", "T125" });
        }
    }
}
