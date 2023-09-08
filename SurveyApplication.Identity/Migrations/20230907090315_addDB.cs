using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApplication.Identity.Migrations
{
    public partial class addDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "a7235612-1c05-4df3-9bd6-d2411abbc9db");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                column: "ConcurrencyStamp",
                value: "a227776f-ccfd-44d2-9416-ecd1ae0696e3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bcbd8ec-f7fc-43d9-8396-c292b09417e5", "AQAAAAEAACcQAAAAEAe9im3EdU2CM+lFDZWzIqpTPOsJF2yR/TtOHHGx365Da9cYq+olPQa3UJG6d1/HzA==", "e617a612-2a7a-4010-b004-e16d7177eb24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c012d2de-7749-42f6-bad9-74bfa54d6485", "AQAAAAEAACcQAAAAEJFaAk9zJgG6Q5Yh9nnjJk3AcF3jl1h4G+pJ6O69m4g2boJJ3aRW2DWHorVcUjZYOg==", "9f51e388-d799-46dd-85fd-fc2e47aac317" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "4e672f4a-2913-4db5-a27c-4bc5037c2954");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                column: "ConcurrencyStamp",
                value: "9bb12345-1dce-4ee2-a8a0-07acaf0ba3e4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9babd503-f2e9-4522-b2a4-d2d01248f784", "AQAAAAEAACcQAAAAEOIbsOlNwQavBQxc8zk2L6HPYXdEhSc83tU4qtFwQruJR2myW6wnHeoD2qlEzS7GCQ==", "b132ea63-533e-4449-85ab-a25224e5cb50" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4ba80bd-3ef0-48b6-b39a-f69c101965c0", "AQAAAAEAACcQAAAAEHDRrXoZnbtZ97+vqyBWoVFDzFAbhwyaaJ52TzJz1xfe3BbYyocSkGYwnG22GuRn+Q==", "644c9fdd-506e-44cf-a000-5f3cbc36eddd" });
        }
    }
}
