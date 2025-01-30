using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Система_за_управление_на_гадатели_MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class StillfixingEnquiryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Enquiries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Enquiries");
        }
    }
}
