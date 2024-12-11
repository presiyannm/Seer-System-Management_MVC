using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Система_за_управление_на_гадатели_MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedEnquiryDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EnquiryCheckInProgress",
                table: "Enquiries",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnquiryCheckInProgress",
                table: "Enquiries");
        }
    }
}
