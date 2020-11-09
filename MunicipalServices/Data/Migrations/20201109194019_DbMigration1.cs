using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MunicipalServices.Data.Migrations
{
    public partial class DbMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "CatchReceipts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    AmountOfMoneyNumber = table.Column<decimal>(nullable: false),
                    AmountOfMoneyText = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    ToAccount = table.Column<string>(nullable: true),
                    Currency = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatchReceipts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CatchReceipts_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    AmountOfMoneyNumber = table.Column<decimal>(nullable: false),
                    AmountOfMoneyText = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    OnAccount = table.Column<string>(nullable: true),
                    ReceivedFrom = table.Column<string>(nullable: true),
                    Currency = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Receipts_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CashiersCheck",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    CatchReceiptID = table.Column<Guid>(nullable: false),
                    ReceiptID = table.Column<Guid>(nullable: false),
                    NumberID = table.Column<int>(nullable: false),
                    CheckNumber = table.Column<int>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: true),
                    CodeBank = table.Column<int>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    CodeBranch = table.Column<int>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    DateOfTheWorthy = table.Column<DateTime>(nullable: false),
                    AmountOfMoney = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashiersCheck", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CashiersCheck_CatchReceipts_CatchReceiptID",
                        column: x => x.CatchReceiptID,
                        principalTable: "CatchReceipts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashiersCheck_Receipts_ReceiptID",
                        column: x => x.ReceiptID,
                        principalTable: "Receipts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashiersCheck_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashiersCheck_CatchReceiptID",
                table: "CashiersCheck",
                column: "CatchReceiptID");

            migrationBuilder.CreateIndex(
                name: "IX_CashiersCheck_ReceiptID",
                table: "CashiersCheck",
                column: "ReceiptID");

            migrationBuilder.CreateIndex(
                name: "IX_CashiersCheck_UserID",
                table: "CashiersCheck",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CatchReceipts_UserID",
                table: "CatchReceipts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_UserID",
                table: "Receipts",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashiersCheck");

            migrationBuilder.DropTable(
                name: "CatchReceipts");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
