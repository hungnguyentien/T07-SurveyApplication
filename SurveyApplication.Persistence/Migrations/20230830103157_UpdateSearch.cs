using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    public partial class UpdateSearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaBangKhaoSat",
                table: "GuiEmail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrangThai",
                table: "DotKhaoSat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrangThai",
                table: "BangKhaoSat",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaBangKhaoSat",
                table: "GuiEmail");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "DotKhaoSat");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "BangKhaoSat");
        }
    }
}
