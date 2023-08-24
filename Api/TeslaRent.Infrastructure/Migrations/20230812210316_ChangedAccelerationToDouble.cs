using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeslaRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAccelerationToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Acceleration",
                table: "CarModels",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Acceleration",
                table: "CarModels",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
