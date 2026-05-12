using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WP.DataAccess.ApplicationDb.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurationClassesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItemsTable_StorageLocation_StorageLocationId",
                table: "InventoryItemsTable");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageLocation_WarehouseSection_WarehouseSectionId",
                table: "StorageLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseSection_Warehouse_WarehouseId",
                table: "WarehouseSection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseSection",
                table: "WarehouseSection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StorageLocation",
                table: "StorageLocation");

            migrationBuilder.DropColumn(
                name: "MaxPalletCapacity",
                table: "Warehouse");

            migrationBuilder.RenameTable(
                name: "WarehouseSection",
                newName: "WarehouseSectionTable");

            migrationBuilder.RenameTable(
                name: "Warehouse",
                newName: "WarehouseTable");

            migrationBuilder.RenameTable(
                name: "StorageLocation",
                newName: "StorageLocationTable");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseSection_WarehouseId",
                table: "WarehouseSectionTable",
                newName: "IX_WarehouseSectionTable_WarehouseId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "WarehouseTable",
                newName: "WarehouseName");

            migrationBuilder.RenameIndex(
                name: "IX_StorageLocation_WarehouseSectionId",
                table: "StorageLocationTable",
                newName: "IX_StorageLocationTable_WarehouseSectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseSectionTable",
                table: "WarehouseSectionTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseTable",
                table: "WarehouseTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StorageLocationTable",
                table: "StorageLocationTable",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Height_Cm_Positive",
                table: "ProductTable",
                sql: "[HeightCm] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Length_Cm_Positive",
                table: "ProductTable",
                sql: "[LengthCm] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Price_Positive",
                table: "ProductTable",
                sql: "[Price] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Weight_Grams_Positive",
                table: "ProductTable",
                sql: "[WeightGrams] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Width_Cm_Positive",
                table: "ProductTable",
                sql: "[WidthCm] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Area_M2_Positive",
                table: "WarehouseSectionTable",
                sql: "[AreaM2] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Max_Pallet_Capacity_Positive",
                table: "WarehouseSectionTable",
                sql: "[MaxPalletCapacity] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Max_Weight_Kg_Positive1",
                table: "WarehouseSectionTable",
                sql: "[MaxWeightKg] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Total_Area_M2_Positive",
                table: "WarehouseTable",
                sql: "[TotalAreaM2] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Max_Pallets_Positive",
                table: "StorageLocationTable",
                sql: "[MaxPallets] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Max_Weight_Kg_Positive",
                table: "StorageLocationTable",
                sql: "[MaxWeightKg] > 0");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItemsTable_StorageLocationTable_StorageLocationId",
                table: "InventoryItemsTable",
                column: "StorageLocationId",
                principalTable: "StorageLocationTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageLocationTable_WarehouseSectionTable_WarehouseSectionId",
                table: "StorageLocationTable",
                column: "WarehouseSectionId",
                principalTable: "WarehouseSectionTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseSectionTable_WarehouseTable_WarehouseId",
                table: "WarehouseSectionTable",
                column: "WarehouseId",
                principalTable: "WarehouseTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItemsTable_StorageLocationTable_StorageLocationId",
                table: "InventoryItemsTable");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageLocationTable_WarehouseSectionTable_WarehouseSectionId",
                table: "StorageLocationTable");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseSectionTable_WarehouseTable_WarehouseId",
                table: "WarehouseSectionTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Height_Cm_Positive",
                table: "ProductTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Length_Cm_Positive",
                table: "ProductTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Price_Positive",
                table: "ProductTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Weight_Grams_Positive",
                table: "ProductTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Width_Cm_Positive",
                table: "ProductTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseTable",
                table: "WarehouseTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Total_Area_M2_Positive",
                table: "WarehouseTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseSectionTable",
                table: "WarehouseSectionTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Area_M2_Positive",
                table: "WarehouseSectionTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Max_Pallet_Capacity_Positive",
                table: "WarehouseSectionTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Max_Weight_Kg_Positive1",
                table: "WarehouseSectionTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StorageLocationTable",
                table: "StorageLocationTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Max_Pallets_Positive",
                table: "StorageLocationTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Max_Weight_Kg_Positive",
                table: "StorageLocationTable");

            migrationBuilder.RenameTable(
                name: "WarehouseTable",
                newName: "Warehouse");

            migrationBuilder.RenameTable(
                name: "WarehouseSectionTable",
                newName: "WarehouseSection");

            migrationBuilder.RenameTable(
                name: "StorageLocationTable",
                newName: "StorageLocation");

            migrationBuilder.RenameColumn(
                name: "WarehouseName",
                table: "Warehouse",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseSectionTable_WarehouseId",
                table: "WarehouseSection",
                newName: "IX_WarehouseSection_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_StorageLocationTable_WarehouseSectionId",
                table: "StorageLocation",
                newName: "IX_StorageLocation_WarehouseSectionId");

            migrationBuilder.AddColumn<int>(
                name: "MaxPalletCapacity",
                table: "Warehouse",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseSection",
                table: "WarehouseSection",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StorageLocation",
                table: "StorageLocation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItemsTable_StorageLocation_StorageLocationId",
                table: "InventoryItemsTable",
                column: "StorageLocationId",
                principalTable: "StorageLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageLocation_WarehouseSection_WarehouseSectionId",
                table: "StorageLocation",
                column: "WarehouseSectionId",
                principalTable: "WarehouseSection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseSection_Warehouse_WarehouseId",
                table: "WarehouseSection",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
