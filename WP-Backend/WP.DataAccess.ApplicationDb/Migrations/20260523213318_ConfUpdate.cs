using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WP.DataAccess.ApplicationDb.Migrations
{
    /// <inheritdoc />
    public partial class ConfUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TenantTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantTable_AspNetUsers_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StorageLeaseTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseSectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedPalletPlaces = table.Column<int>(type: "int", nullable: false),
                    RequestedAreaM2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MonthlyPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaseStatus = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageLeaseTable", x => x.Id);
                    table.CheckConstraint("CK_Monthly_Price_Positive", "[MonthlyPrice] > 0");
                    table.CheckConstraint("CK_Requested_Area_M2_Positive", "[RequestedAreaM2] > 0");
                    table.CheckConstraint("CK_Requested_Pallet_Places_Positive", "[RequestedPalletPlaces] > 0");
                    table.ForeignKey(
                        name: "FK_StorageLeaseTable_TenantTable_TenantId",
                        column: x => x.TenantId,
                        principalTable: "TenantTable",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StorageLeaseTable_WarehouseSectionTable_WarehouseSectionId",
                        column: x => x.WarehouseSectionId,
                        principalTable: "WarehouseSectionTable",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StorageLeaseTable_WarehouseTable_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "WarehouseTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StorageLeaseTable_TenantId",
                table: "StorageLeaseTable",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageLeaseTable_WarehouseId",
                table: "StorageLeaseTable",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageLeaseTable_WarehouseSectionId",
                table: "StorageLeaseTable",
                column: "WarehouseSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantTable_UserAccountId",
                table: "TenantTable",
                column: "UserAccountId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StorageLeaseTable");

            migrationBuilder.DropTable(
                name: "TenantTable");
        }
    }
}
