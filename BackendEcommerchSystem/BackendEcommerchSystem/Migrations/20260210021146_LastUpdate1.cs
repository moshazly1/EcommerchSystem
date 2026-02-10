using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendEcommerchSystem.Migrations
{
    /// <inheritdoc />
    public partial class LastUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_subcategories_SubCategoryId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "products",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_products_CategoryId",
                table: "products",
                newName: "IX_products_BrandId");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryId",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_products_Brand_BrandId",
                table: "products",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_subcategories_SubCategoryId",
                table: "products",
                column: "SubCategoryId",
                principalTable: "subcategories",
                principalColumn: "SubCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_Brand_BrandId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_subcategories_SubCategoryId",
                table: "products");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_products_BrandId",
                table: "products",
                newName: "IX_products_CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryId",
                table: "products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_subcategories_SubCategoryId",
                table: "products",
                column: "SubCategoryId",
                principalTable: "subcategories",
                principalColumn: "SubCategoryId");
        }
    }
}
