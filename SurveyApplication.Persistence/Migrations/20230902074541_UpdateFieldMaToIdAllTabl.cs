using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    public partial class UpdateFieldMaToIdAllTabl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaDonVi",
                table: "NguoiDaiDien",
                newName: "IdDonVi");

            migrationBuilder.RenameColumn(
                name: "MaBangKhaoSat",
                table: "GuiEmail",
                newName: "IdBangKhaoSat");

            migrationBuilder.RenameColumn(
                name: "MaLoaiHinh",
                table: "DotKhaoSat",
                newName: "IdLoaiHinh");

            migrationBuilder.RenameColumn(
                name: "MaLoaiHinh",
                table: "BangKhaoSat",
                newName: "IdLoaiHinh");

            migrationBuilder.RenameColumn(
                name: "MaDotKhaoSat",
                table: "BangKhaoSat",
                newName: "IdDotKhaoSat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdDonVi",
                table: "NguoiDaiDien",
                newName: "MaDonVi");

            migrationBuilder.RenameColumn(
                name: "IdBangKhaoSat",
                table: "GuiEmail",
                newName: "MaBangKhaoSat");

            migrationBuilder.RenameColumn(
                name: "IdLoaiHinh",
                table: "DotKhaoSat",
                newName: "MaLoaiHinh");

            migrationBuilder.RenameColumn(
                name: "IdLoaiHinh",
                table: "BangKhaoSat",
                newName: "MaLoaiHinh");

            migrationBuilder.RenameColumn(
                name: "IdDotKhaoSat",
                table: "BangKhaoSat",
                newName: "MaDotKhaoSat");
        }
    }
}
