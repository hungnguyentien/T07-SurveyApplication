using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    public partial class AddColumnIdDonViTblBaoCao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdDonVi",
                table: "BaoCaoCauHoi",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdDonVi",
                table: "BaoCaoCauHoi");
        }
    }
}
