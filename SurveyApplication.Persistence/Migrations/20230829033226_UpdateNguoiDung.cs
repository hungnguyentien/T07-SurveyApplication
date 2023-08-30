using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    public partial class UpdateNguoiDung : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenNguoiDung",
                table: "NguoiDung",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "NguoiDung",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HoTen",
                table: "NguoiDung",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgaySinh",
                table: "NguoiDung",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SoDienThoai",
                table: "NguoiDung",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "NguoiDung");

            migrationBuilder.DropColumn(
                name: "HoTen",
                table: "NguoiDung");

            migrationBuilder.DropColumn(
                name: "NgaySinh",
                table: "NguoiDung");

            migrationBuilder.DropColumn(
                name: "SoDienThoai",
                table: "NguoiDung");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "NguoiDung",
                newName: "TenNguoiDung");
        }
    }
}
