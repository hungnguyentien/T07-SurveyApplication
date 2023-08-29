using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    public partial class UpdateTableCot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOther",
                table: "Hang");

            migrationBuilder.DropColumn(
                name: "LabelCauTraLoi",
                table: "Hang");

            migrationBuilder.AlterColumn<string>(
                name: "NoiDung",
                table: "Cot",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOther",
                table: "Cot");

            migrationBuilder.DropColumn(
                name: "LabelCauTraLoi",
                table: "Cot");

            migrationBuilder.AddColumn<bool>(
                name: "IsOther",
                table: "Hang",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LabelCauTraLoi",
                table: "Hang",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "NoiDung",
                table: "Cot",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
