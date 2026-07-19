using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendEcommerchSystem.Migrations
{
    /// <inheritdoc />
    public partial class WhiteList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WhiteLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhiteLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WhiteLists_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WhiteLists_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 4, 10, 50, 30, 628, DateTimeKind.Local).AddTicks(4515), "$2a$11$a1YQ/osp5WiqS6vMMJ3bZOXVHFPyx2QIZhHvDt8V15VrtMR8L4UVC" });

            migrationBuilder.CreateIndex(
                name: "IX_WhiteLists_ProductId",
                table: "WhiteLists",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WhiteLists_UserId",
                table: "WhiteLists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WhiteLists");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 6, 18, 11, 18, 50, 987, DateTimeKind.Local).AddTicks(3146), "$2a$11$yDvv6Dvhjc5nkjC21/hzXOlMwyPrrx7cP32ybjGPnSX0jrWDtN9jG" });
        }
    }
}
