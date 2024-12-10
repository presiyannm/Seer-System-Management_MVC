using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Система_за_управление_на_гадатели_MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EnquiryStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnquiryStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnquiryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnquiryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enquiries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnquiryTypeId = table.Column<int>(type: "int", nullable: false),
                    EnquiryStatusId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SeerId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserBirthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnquirySentToCheck = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnquiryCheckFinished = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enquiries_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enquiries_EnquiryStatuses_EnquiryStatusId",
                        column: x => x.EnquiryStatusId,
                        principalTable: "EnquiryStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enquiries_EnquiryTypes_EnquiryTypeId",
                        column: x => x.EnquiryTypeId,
                        principalTable: "EnquiryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enquiries_Seers_SeerId",
                        column: x => x.SeerId,
                        principalTable: "Seers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "EnquiryStatuses",
                columns: new[] { "Id", "Status" },
                values: new object[,]
                {
                    { 1, "чакащ" },
                    { 2, "за преглед" },
                    { 3, "в процес" },
                    { 4, "изпълнен" },
                    { 5, "отказан" }
                });

            migrationBuilder.InsertData(
                table: "EnquiryTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "хороскоп" },
                    { 2, "китайски хороскоп" },
                    { 3, "лунен хороскоп" },
                    { 4, "таро" },
                    { 5, "руни" },
                    { 6, "тао оракул" },
                    { 7, "гласът на провидението" },
                    { 8, "друга" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_ApplicationUserId",
                table: "Enquiries",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_EnquiryStatusId",
                table: "Enquiries",
                column: "EnquiryStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_EnquiryTypeId",
                table: "Enquiries",
                column: "EnquiryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_SeerId",
                table: "Enquiries",
                column: "SeerId");

            migrationBuilder.CreateIndex(
                name: "IX_Seers_ApplicationUserId",
                table: "Seers",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enquiries");

            migrationBuilder.DropTable(
                name: "EnquiryStatuses");

            migrationBuilder.DropTable(
                name: "EnquiryTypes");

            migrationBuilder.DropTable(
                name: "Seers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
