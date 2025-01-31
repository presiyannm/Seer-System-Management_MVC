using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Система_за_управление_на_гадатели_MVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSeer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Seers",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SumOfRating",
                table: "Seers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SumOfRating",
                table: "Seers");

            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "Seers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
