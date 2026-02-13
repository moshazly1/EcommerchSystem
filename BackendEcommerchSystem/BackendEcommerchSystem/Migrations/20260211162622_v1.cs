using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendEcommerchSystem.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsImage",
                table: "ProductImages");

            migrationBuilder.AddColumn<int>(
                name: "ImageType",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageType",
                table: "ProductImages");

            migrationBuilder.AddColumn<bool>(
                name: "IsImage",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
