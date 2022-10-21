using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_System.Migrations
{
    public partial class ALTERTABLESREL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoursesId",
                table: "Resources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoursesId",
                table: "Homework",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentsId",
                table: "Homework",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CoursesId",
                table: "Resources",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_Homework_CoursesId",
                table: "Homework",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_Homework_StudentsId",
                table: "Homework",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homework_Courses_CoursesId",
                table: "Homework",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Homework_Students_StudentsId",
                table: "Homework",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Courses_CoursesId",
                table: "Resources",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homework_Courses_CoursesId",
                table: "Homework");

            migrationBuilder.DropForeignKey(
                name: "FK_Homework_Students_StudentsId",
                table: "Homework");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Courses_CoursesId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_CoursesId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Homework_CoursesId",
                table: "Homework");

            migrationBuilder.DropIndex(
                name: "IX_Homework_StudentsId",
                table: "Homework");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "Homework");

            migrationBuilder.DropColumn(
                name: "StudentsId",
                table: "Homework");
        }
    }
}
