using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MunicipalServices.Data.Migrations
{
    public partial class DbMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComplaintForm",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    IdentificationNumber = table.Column<string>(nullable: true),
                    AreaName = table.Column<string>(nullable: true),
                    FullAddress = table.Column<string>(nullable: true),
                    ComplaintType = table.Column<int>(nullable: false),
                    SubjectComplaint = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintForm", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ComplaintForm_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionLicense",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    DateOfIssuance = table.Column<DateTime>(nullable: false),
                    FileNumber = table.Column<int>(nullable: false),
                    ValidatedBySessionNumber = table.Column<int>(nullable: false),
                    TownName = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    Basin = table.Column<int>(nullable: false),
                    Part = table.Column<int>(nullable: false),
                    ConstructionUse = table.Column<string>(nullable: true),
                    ConstructionLicenseType = table.Column<int>(nullable: false),
                    LocalCommitteeNumber = table.Column<int>(nullable: false),
                    DateApprovalLocalCommittee = table.Column<DateTime>(nullable: false),
                    LicenseDescription = table.Column<string>(nullable: true),
                    LicenseConditions = table.Column<string>(nullable: true),
                    FeeDate = table.Column<DateTime>(nullable: false),
                    RemainingFeesDate = table.Column<DateTime>(nullable: false),
                    BillOfFeesID = table.Column<Guid>(nullable: true),
                    BillRemainingFeesID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionLicense", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConstructionLicense_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CraftAndIndustryLicense",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    LicenseHolderName = table.Column<string>(nullable: true),
                    IdentificationNumber = table.Column<string>(nullable: true),
                    CraftOrIndustryType = table.Column<string>(nullable: true),
                    ClassifiedInTail = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Ends = table.Column<DateTime>(nullable: false),
                    LicenseFee = table.Column<decimal>(nullable: false),
                    VoucherNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CraftAndIndustryLicense", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CraftAndIndustryLicense_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VacationRequest",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    DaysVacation = table.Column<int>(nullable: false),
                    StartVacationDate = table.Column<DateTime>(nullable: false),
                    EndVacationDate = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    NameAssignee = table.Column<string>(nullable: true),
                    Agree = table.Column<bool>(nullable: false),
                    VacationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VacationRequest_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WaterMeterSubscriptionRequest",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    BasinNumber = table.Column<int>(nullable: false),
                    PieceNumber = table.Column<int>(nullable: false),
                    WaterOfficialSuggestions = table.Column<string>(nullable: true),
                    MunicipalityDecision = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterMeterSubscriptionRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WaterMeterSubscriptionRequest_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LicenseHolderInformation",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    ConstructionLicenseID = table.Column<Guid>(nullable: false),
                    NameLicenseHolder = table.Column<string>(nullable: true),
                    IdentityNumberLicenseHolder = table.Column<string>(nullable: true),
                    AddressLicenseHolder = table.Column<string>(nullable: true),
                    PhoneNumberLicenseHolder = table.Column<string>(nullable: true),
                    NameDesigningOffice = table.Column<string>(nullable: true),
                    IdentityNumberDesignerOffice = table.Column<string>(nullable: true),
                    AddressDesignerOffice = table.Column<string>(nullable: true),
                    PhoneNumberDesignerOffice = table.Column<string>(nullable: true),
                    NameSupervisingEngineer = table.Column<string>(nullable: true),
                    IdentityNumberSupervisingEngineer = table.Column<string>(nullable: true),
                    AddressSupervisingEngineer = table.Column<string>(nullable: true),
                    PhoneNumberSupervisingEngineer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseHolderInformation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LicenseHolderInformation_ConstructionLicense_ConstructionLicenseID",
                        column: x => x.ConstructionLicenseID,
                        principalTable: "ConstructionLicense",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicenseHolderInformation_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintForm_UserID",
                table: "ComplaintForm",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionLicense_UserID",
                table: "ConstructionLicense",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CraftAndIndustryLicense_UserID",
                table: "CraftAndIndustryLicense",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseHolderInformation_ConstructionLicenseID",
                table: "LicenseHolderInformation",
                column: "ConstructionLicenseID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LicenseHolderInformation_UserID",
                table: "LicenseHolderInformation",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequest_UserID",
                table: "VacationRequest",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WaterMeterSubscriptionRequest_UserID",
                table: "WaterMeterSubscriptionRequest",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplaintForm");

            migrationBuilder.DropTable(
                name: "CraftAndIndustryLicense");

            migrationBuilder.DropTable(
                name: "LicenseHolderInformation");

            migrationBuilder.DropTable(
                name: "VacationRequest");

            migrationBuilder.DropTable(
                name: "WaterMeterSubscriptionRequest");

            migrationBuilder.DropTable(
                name: "ConstructionLicense");
        }
    }
}
