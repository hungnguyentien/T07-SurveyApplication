using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    public partial class UpdateTblCauHoiRemoveFieldBatBuoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatBuoc",
                table: "CauHoi");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "CauHoi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BatBuoc",
                table: "CauHoi",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "CauHoi",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
