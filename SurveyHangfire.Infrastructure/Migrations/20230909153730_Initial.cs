using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hangfire.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTypeId = table.Column<int>(nullable: false),
                    JobName = table.Column<string>(nullable: false),
                    CronString = table.Column<string>(nullable: true),
                    Service = table.Column<string>(nullable: true),
                    ApiUrl = table.Column<string>(nullable: true),
                    Interval = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSchedule", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobSchedule");
        }
    }
}
