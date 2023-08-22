using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeslaRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCostAndMileage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "EndMileage",
                table: "Reservations",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "StartMileage",
                table: "Reservations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCost",
                table: "Reservations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CostPerDay",
                table: "CarModels",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CostPerMonth",
                table: "CarModels",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CostPerWeek",
                table: "CarModels",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndMileage",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "StartMileage",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CostPerDay",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "CostPerMonth",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "CostPerWeek",
                table: "CarModels");
        }
    }
}
