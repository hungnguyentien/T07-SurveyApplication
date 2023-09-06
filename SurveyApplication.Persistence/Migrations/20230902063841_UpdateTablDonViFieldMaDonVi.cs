using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    public partial class UpdateTablDonViFieldMaDonVi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaLoaiHinh",
                table: "DonVi",
                newName: "IdLoaiHinh");

            migrationBuilder.RenameColumn(
                name: "MaLinhVuc",
                table: "DonVi",
                newName: "IdLinhVuc");

            migrationBuilder.AlterColumn<string>(
                name: "MaDonVi",
                table: "DonVi",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdLoaiHinh",
                table: "DonVi",
                newName: "MaLoaiHinh");

            migrationBuilder.RenameColumn(
                name: "IdLinhVuc",
                table: "DonVi",
                newName: "MaLinhVuc");

            migrationBuilder.AlterColumn<Guid>(
                name: "MaDonVi",
                table: "DonVi",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
