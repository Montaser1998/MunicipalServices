using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MunicipalServices.Data.Migrations
{
    public partial class DbMigration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastDateLogined",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastDateLogined",
                table: "AspNetUsers");
        }
    }
}
