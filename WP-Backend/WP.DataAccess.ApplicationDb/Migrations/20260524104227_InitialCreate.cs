using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WP.DataAccess.ApplicationDb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WarehouseSectionTable_WarehouseId",
                table: "WarehouseSectionTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Max_Weight_Kg_Positive1",
                table: "WarehouseSectionTable");

            migrationBuilder.DropIndex(
                name: "IX_StorageLocationTable_WarehouseSectionId",
                table: "StorageLocationTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Max_Weight_Kg_Positive",
                table: "StorageLocationTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Inventory_ReservedUnits_Positive",
                table: "InventoryItemsTable");

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseName",
                table: "WarehouseTable",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "WarehouseTable",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WarehouseSectionTable",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "WarehouseSectionTable",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TenantTable",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactPerson",
                table: "TenantTable",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "TenantTable",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "TenantTable",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "StorageLocationTable",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "StorageLeaseTable",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SKU",
                table: "ProductTable",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "ProductTable",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductTable",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BarCode",
                table: "ProductTable",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PalletName",
                table: "PalletTypesTable",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "PackagingProfilesTable",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ManufacturerName",
                table: "ManufacturerTable",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "ManufacturerTable",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactPhone",
                table: "ManufacturerTable",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactEmail",
                table: "ManufacturerTable",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "ManufacturerTable",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BatchNumber",
                table: "InventoryItemsTable",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CategoryTable",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "CategoryTable",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseTable_WarehouseName",
                table: "WarehouseTable",
                column: "WarehouseName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_WarehouseSection_WarehouseId_Code",
                table: "WarehouseSectionTable",
                columns: new[] { "WarehouseId", "Code" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_WarehouseSection_MaxWeightKg_Positive",
                table: "WarehouseSectionTable",
                sql: "[MaxWeightKg] > 0");

            migrationBuilder.CreateIndex(
                name: "UX_StorageLocation_WarehouseSectionId_Code",
                table: "StorageLocationTable",
                columns: new[] { "WarehouseSectionId", "Code" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_StorageLocation_MaxWeightKg_Positive",
                table: "StorageLocationTable",
                sql: "[MaxWeightKg] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_StorageLease_EndDate_After_StartDate",
                table: "StorageLeaseTable",
                sql: "[EndDate] > [StartDate]");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTable_BarCode",
                table: "ProductTable",
                column: "BarCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTable_SKU",
                table: "ProductTable",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PalletTypesTable_PalletName",
                table: "PalletTypesTable",
                column: "PalletName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturerTable_ManufacturerName",
                table: "ManufacturerTable",
                column: "ManufacturerName",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Inventory_ReservedUnits_LessOrEqual_QuantityUnits",
                table: "InventoryItemsTable",
                sql: "[ReservedUnits] <= [QuantityUnits]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Inventory_ReservedUnits_NonNegative",
                table: "InventoryItemsTable",
                sql: "[ReservedUnits] >= 0");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTable_CategoryName",
                table: "CategoryTable",
                column: "CategoryName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WarehouseTable_WarehouseName",
                table: "WarehouseTable");

            migrationBuilder.DropIndex(
                name: "UX_WarehouseSection_WarehouseId_Code",
                table: "WarehouseSectionTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_WarehouseSection_MaxWeightKg_Positive",
                table: "WarehouseSectionTable");

            migrationBuilder.DropIndex(
                name: "UX_StorageLocation_WarehouseSectionId_Code",
                table: "StorageLocationTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_StorageLocation_MaxWeightKg_Positive",
                table: "StorageLocationTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_StorageLease_EndDate_After_StartDate",
                table: "StorageLeaseTable");

            migrationBuilder.DropIndex(
                name: "IX_ProductTable_BarCode",
                table: "ProductTable");

            migrationBuilder.DropIndex(
                name: "IX_ProductTable_SKU",
                table: "ProductTable");

            migrationBuilder.DropIndex(
                name: "IX_PalletTypesTable_PalletName",
                table: "PalletTypesTable");

            migrationBuilder.DropIndex(
                name: "IX_ManufacturerTable_ManufacturerName",
                table: "ManufacturerTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Inventory_ReservedUnits_LessOrEqual_QuantityUnits",
                table: "InventoryItemsTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Inventory_ReservedUnits_NonNegative",
                table: "InventoryItemsTable");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTable_CategoryName",
                table: "CategoryTable");

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseName",
                table: "WarehouseTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "WarehouseTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WarehouseSectionTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "WarehouseSectionTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TenantTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactPerson",
                table: "TenantTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "TenantTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "TenantTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "StorageLocationTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "StorageLeaseTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SKU",
                table: "ProductTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "ProductTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BarCode",
                table: "ProductTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PalletName",
                table: "PalletTypesTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "PackagingProfilesTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ManufacturerName",
                table: "ManufacturerTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "ManufacturerTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactPhone",
                table: "ManufacturerTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactEmail",
                table: "ManufacturerTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "ManufacturerTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BatchNumber",
                table: "InventoryItemsTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CategoryTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "CategoryTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseSectionTable_WarehouseId",
                table: "WarehouseSectionTable",
                column: "WarehouseId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Max_Weight_Kg_Positive1",
                table: "WarehouseSectionTable",
                sql: "[MaxWeightKg] > 0");

            migrationBuilder.CreateIndex(
                name: "IX_StorageLocationTable_WarehouseSectionId",
                table: "StorageLocationTable",
                column: "WarehouseSectionId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Max_Weight_Kg_Positive",
                table: "StorageLocationTable",
                sql: "[MaxWeightKg] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Inventory_ReservedUnits_Positive",
                table: "InventoryItemsTable",
                sql: "[ReservedUnits] > 0");
        }
    }
}
