using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    public partial class UpdateReleaseHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IDChannel",
                table: "ReleaseHistory",
                newName: "IdChannel");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ReleaseHistory",
                newName: "Id");

            migrationBuilder.AlterColumn<decimal>(
                name: "Size",
                table: "StgFile",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdChannel",
                table: "ReleaseHistory",
                newName: "IDChannel");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ReleaseHistory",
                newName: "ID");

            migrationBuilder.AlterColumn<decimal>(
                name: "Size",
                table: "StgFile",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");
        }
    }
}
