using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    public partial class RemoveFieldTblCot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOther",
                table: "Cot");

            migrationBuilder.DropColumn(
                name: "LabelCauTraLoi",
                table: "Cot");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOther",
                table: "Cot",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LabelCauTraLoi",
                table: "Cot",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
