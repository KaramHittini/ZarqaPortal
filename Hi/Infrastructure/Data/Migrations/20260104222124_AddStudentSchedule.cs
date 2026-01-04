using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZarqaPortal.Web.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentSchedules_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSchedules_CourseId",
                table: "StudentSchedules",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSchedules_Username_CourseId",
                table: "StudentSchedules",
                columns: new[] { "Username", "CourseId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentSchedules");
        }
    }
}
