using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Система_за_управление_на_гадатели_MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixingSeerEnquiryModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Seers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Enquiries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "WantedResult",
                table: "Enquiries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Seers");

            migrationBuilder.DropColumn(
                name: "WantedResult",
                table: "Enquiries");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Enquiries",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
