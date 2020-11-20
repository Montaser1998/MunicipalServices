using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MunicipalServices.Data.Migrations
{
    public partial class DbMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashiersCheck_CatchReceipts_CatchReceiptID",
                table: "CashiersCheck");

            migrationBuilder.DropForeignKey(
                name: "FK_CashiersCheck_Receipts_ReceiptID",
                table: "CashiersCheck");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReceiptID",
                table: "CashiersCheck",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CatchReceiptID",
                table: "CashiersCheck",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_CashiersCheck_CatchReceipts_CatchReceiptID",
                table: "CashiersCheck",
                column: "CatchReceiptID",
                principalTable: "CatchReceipts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CashiersCheck_Receipts_ReceiptID",
                table: "CashiersCheck",
                column: "ReceiptID",
                principalTable: "Receipts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashiersCheck_CatchReceipts_CatchReceiptID",
                table: "CashiersCheck");

            migrationBuilder.DropForeignKey(
                name: "FK_CashiersCheck_Receipts_ReceiptID",
                table: "CashiersCheck");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReceiptID",
                table: "CashiersCheck",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CatchReceiptID",
                table: "CashiersCheck",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CashiersCheck_CatchReceipts_CatchReceiptID",
                table: "CashiersCheck",
                column: "CatchReceiptID",
                principalTable: "CatchReceipts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CashiersCheck_Receipts_ReceiptID",
                table: "CashiersCheck",
                column: "ReceiptID",
                principalTable: "Receipts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
