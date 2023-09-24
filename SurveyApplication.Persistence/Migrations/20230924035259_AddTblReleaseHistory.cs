using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    public partial class AddTblReleaseHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReleaseHistory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDChannel = table.Column<int>(type: "int", nullable: false),
                    BuildNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MigrationId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Env = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseHistory", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReleaseHistory");
        }
    }
}
