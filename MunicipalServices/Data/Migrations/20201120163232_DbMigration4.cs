using Microsoft.EntityFrameworkCore.Migrations;

namespace MunicipalServices.Data.Migrations
{
    public partial class DbMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberID",
                table: "CashiersCheck");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberID",
                table: "CashiersCheck",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
