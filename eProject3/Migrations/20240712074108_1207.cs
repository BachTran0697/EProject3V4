using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eProject3.Migrations
{
    /// <inheritdoc />
    public partial class _1207 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cancellations");

            migrationBuilder.AlterColumn<int>(
                name: "SeatId",
                table: "SeatDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayStatus",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "PayStatus",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayStatus",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "SeatId",
                table: "SeatDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Cancellations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CancellationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CancellationFee = table.Column<float>(type: "real", nullable: false),
                    Ticket_code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cancellations", x => x.Id);
                });
        }
    }
}
