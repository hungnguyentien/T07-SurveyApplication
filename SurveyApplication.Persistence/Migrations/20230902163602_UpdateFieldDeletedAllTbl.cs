using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    public partial class UpdateFieldDeletedAllTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "VaiTroQuyen",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "VaiTro",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Quyen",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "NguoiDungVaiTro",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "NguoiDung",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "NguoiDaiDien",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "LoaiHinhDonVi",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "LinhVucHoatDong",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "KetQua",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Hang",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "GuiEmail",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "DotKhaoSat",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "DonVi",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Cot",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "CauHoi",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "BangKhaoSatCauHoi",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "BangKhaoSat",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "VaiTroQuyen");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "VaiTro");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Quyen");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "NguoiDungVaiTro");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "NguoiDung");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "NguoiDaiDien");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "LoaiHinhDonVi");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "LinhVucHoatDong");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "KetQua");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Hang");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "GuiEmail");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "DotKhaoSat");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "DonVi");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Cot");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "CauHoi");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "BangKhaoSatCauHoi");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "BangKhaoSat");
        }
    }
}
