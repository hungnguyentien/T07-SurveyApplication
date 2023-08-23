using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    public partial class UpdateCauHoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaLoaiCauHoi",
                table: "CauHois",
                newName: "TieuDe");

            migrationBuilder.AlterColumn<int>(
                name: "MaVaiTro",
                table: "VaiTroQuyens",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MaQuyen",
                table: "VaiTroQuyens",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MaVaiTro",
                table: "NguoiDungVaiTros",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MaNguoiDung",
                table: "NguoiDungVaiTros",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "MaDonVi",
                table: "NguoiDaiDiens",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "MaLoaiHinh",
                table: "DotKhaoSats",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MaLoaiHinh",
                table: "DonVis",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MaLinhVuc",
                table: "DonVis",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "KichThuocFile",
                table: "CauHois",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "LoaiCauHoi",
                table: "CauHois",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "NoiDung",
                table: "CauHois",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SoLuongFileToiDa",
                table: "CauHois",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "MaLoaiHinh",
                table: "BangKhaoSats",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MaDotKhaoSat",
                table: "BangKhaoSats",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "CauHoiPhus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaCauHoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCauHoi = table.Column<int>(type: "int", nullable: false),
                    ActiveFlag = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHoiPhus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DapAns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDapAn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCauHoi = table.Column<int>(type: "int", nullable: false),
                    ActiveFlag = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DapAns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KetQuas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdDonVi = table.Column<int>(type: "int", nullable: false),
                    IdNguoiDaiDien = table.Column<int>(type: "int", nullable: false),
                    IdBangKhaoSat = table.Column<int>(type: "int", nullable: false),
                    ActiveFlag = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KetQuas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CauHoiPhus");

            migrationBuilder.DropTable(
                name: "DapAns");

            migrationBuilder.DropTable(
                name: "KetQuas");

            migrationBuilder.DropColumn(
                name: "KichThuocFile",
                table: "CauHois");

            migrationBuilder.DropColumn(
                name: "LoaiCauHoi",
                table: "CauHois");

            migrationBuilder.DropColumn(
                name: "NoiDung",
                table: "CauHois");

            migrationBuilder.DropColumn(
                name: "SoLuongFileToiDa",
                table: "CauHois");

            migrationBuilder.RenameColumn(
                name: "TieuDe",
                table: "CauHois",
                newName: "MaLoaiCauHoi");

            migrationBuilder.AlterColumn<int>(
                name: "MaVaiTro",
                table: "VaiTroQuyens",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaQuyen",
                table: "VaiTroQuyens",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaVaiTro",
                table: "NguoiDungVaiTros",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaNguoiDung",
                table: "NguoiDungVaiTros",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MaDonVi",
                table: "NguoiDaiDiens",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaLoaiHinh",
                table: "DotKhaoSats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaLoaiHinh",
                table: "DonVis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaLinhVuc",
                table: "DonVis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaLoaiHinh",
                table: "BangKhaoSats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaDotKhaoSat",
                table: "BangKhaoSats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
