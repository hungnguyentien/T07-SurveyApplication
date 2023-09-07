using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    public partial class UpdateTblKetQuaTrangThai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdBangKhaoSat",
                table: "KetQua");

            migrationBuilder.DropColumn(
                name: "IdDonVi",
                table: "KetQua");

            migrationBuilder.RenameColumn(
                name: "IdNguoiDaiDien",
                table: "KetQua",
                newName: "TrangThai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrangThai",
                table: "KetQua",
                newName: "IdNguoiDaiDien");

            migrationBuilder.AddColumn<int>(
                name: "IdBangKhaoSat",
                table: "KetQua",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdDonVi",
                table: "KetQua",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
