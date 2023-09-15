using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Persistence.Migrations
{
    public partial class AddIpAddressClientTblKq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IpAddressClient",
                table: "KetQua",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpAddressClient",
                table: "KetQua");
        }
    }
}
