using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WP.DataAccess.ApplicationDb.Migrations
{
    /// <inheritdoc />
    public partial class EntityUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserTypes",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserTypes",
                table: "Role");
        }
    }
}
