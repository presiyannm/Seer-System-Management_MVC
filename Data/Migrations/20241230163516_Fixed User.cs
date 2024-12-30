using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Система_за_управление_на_гадатели_MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Seers_ApplicationUserId",
                table: "Seers");

            migrationBuilder.CreateIndex(
                name: "IX_Seers_ApplicationUserId",
                table: "Seers",
                column: "ApplicationUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Seers_ApplicationUserId",
                table: "Seers");

            migrationBuilder.CreateIndex(
                name: "IX_Seers_ApplicationUserId",
                table: "Seers",
                column: "ApplicationUserId");
        }
    }
}
