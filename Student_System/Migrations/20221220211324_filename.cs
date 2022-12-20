using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_System.Migrations
{
    public partial class filename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Emails",
                table: "Students",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Homework",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Homework");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Students",
                newName: "Emails");
        }
    }
}
