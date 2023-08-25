using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    public partial class UpdateTableCauHoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOther",
                table: "CauHoi",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LabelCauTraLoi",
                table: "CauHoi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOther",
                table: "CauHoi");

            migrationBuilder.DropColumn(
                name: "LabelCauTraLoi",
                table: "CauHoi");
        }
    }
}
