using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MunicipalServices.Data.Migrations
{
    public partial class DbMigration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConstructionDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    ConstructionLicenseID = table.Column<Guid>(nullable: false),
                    ConstructionDescription = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    MeasruingUnit = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConstructionDetails_ConstructionLicense_ConstructionLicenseID",
                        column: x => x.ConstructionLicenseID,
                        principalTable: "ConstructionLicense",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConstructionDetails_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionDetails_ConstructionLicenseID",
                table: "ConstructionDetails",
                column: "ConstructionLicenseID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionDetails_UserID",
                table: "ConstructionDetails",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstructionDetails");
        }
    }
}
