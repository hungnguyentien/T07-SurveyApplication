using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    public partial class AddId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdQuanHuyen",
                table: "DonVi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdTinhTp",
                table: "DonVi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdXaPhuong",
                table: "DonVi",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdQuanHuyen",
                table: "DonVi");

            migrationBuilder.DropColumn(
                name: "IdTinhTp",
                table: "DonVi");

            migrationBuilder.DropColumn(
                name: "IdXaPhuong",
                table: "DonVi");
        }
    }
}
