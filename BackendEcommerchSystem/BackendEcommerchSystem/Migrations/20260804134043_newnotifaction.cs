using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendEcommerchSystem.Migrations
{
    /// <inheritdoc />
    public partial class newnotifaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailDigest",
                table: "users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "EmailDigest", "PasswordHash" },
                values: new object[] { new DateTime(2026, 8, 4, 16, 40, 41, 139, DateTimeKind.Local).AddTicks(9035), true, "$2a$11$CF7TE0cfx7kcWn.Lrsm9c.0wlZ3qIr6U9Dj6KnL7PvCs/dWHZ4Fc." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailDigest",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 31, 13, 4, 59, 409, DateTimeKind.Local).AddTicks(1460), "$2a$11$W.NPXl4Sh9MQEtQb2ZlabO6Uy8M5Dx6WL.4OYd9mHWmnlCttMuUMe" });
        }
    }
}
