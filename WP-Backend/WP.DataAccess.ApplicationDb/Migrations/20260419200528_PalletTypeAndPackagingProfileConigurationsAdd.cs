using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WP.DataAccess.ApplicationDb.Migrations
{
    /// <inheritdoc />
    public partial class PalletTypeAndPackagingProfileConigurationsAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTable_PackagingProfile_PackagingProfileId",
                table: "ProductTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackagingProfile",
                table: "PackagingProfile");

            migrationBuilder.RenameTable(
                name: "PackagingProfile",
                newName: "PackagingProfilesTable");

            migrationBuilder.RenameColumn(
                name: "WeightKg",
                table: "ProductTable",
                newName: "WeightGrams");

            migrationBuilder.RenameColumn(
                name: "PackagingWeightKg",
                table: "PackagingProfilesTable",
                newName: "PackagingWeightGrams");

            migrationBuilder.RenameColumn(
                name: "MaxStackCount",
                table: "PackagingProfilesTable",
                newName: "MaximumStackCount");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackagingProfilesTable",
                table: "PackagingProfilesTable",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PalletTypesTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PalletName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PalletStandard = table.Column<int>(type: "int", nullable: false),
                    LengthCm = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    WidthCm = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MaxLoadGrams = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxHeightCm = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PalletTypesTable", x => x.Id);
                    table.CheckConstraint("CK_Pallet_Length_Positive", "[LengthCm] > 0");
                    table.CheckConstraint("CK_Pallet_MaxLoad_Positive", "[MaxLoadGrams] > 0");
                    table.CheckConstraint("CK_Pallet_Width_Positive", "[WidthCm] > 0");
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTable_PackagingProfilesTable_PackagingProfileId",
                table: "ProductTable",
                column: "PackagingProfileId",
                principalTable: "PackagingProfilesTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTable_PackagingProfilesTable_PackagingProfileId",
                table: "ProductTable");

            migrationBuilder.DropTable(
                name: "PalletTypesTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackagingProfilesTable",
                table: "PackagingProfilesTable");

            migrationBuilder.RenameTable(
                name: "PackagingProfilesTable",
                newName: "PackagingProfile");

            migrationBuilder.RenameColumn(
                name: "WeightGrams",
                table: "ProductTable",
                newName: "WeightKg");

            migrationBuilder.RenameColumn(
                name: "PackagingWeightGrams",
                table: "PackagingProfile",
                newName: "PackagingWeightKg");

            migrationBuilder.RenameColumn(
                name: "MaximumStackCount",
                table: "PackagingProfile",
                newName: "MaxStackCount");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackagingProfile",
                table: "PackagingProfile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTable_PackagingProfile_PackagingProfileId",
                table: "ProductTable",
                column: "PackagingProfileId",
                principalTable: "PackagingProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
