using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendEcommerchSystem.Migrations
{
    /// <inheritdoc />
    public partial class newnotofications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AccountActivity",
                table: "users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SmsUpdates",
                table: "users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AccountActivity", "CreatedAt", "PasswordHash", "SmsUpdates" },
                values: new object[] { true, new DateTime(2026, 8, 5, 10, 26, 47, 246, DateTimeKind.Local).AddTicks(3031), "$2a$11$swlA7RxAKxZWSyWzvtwPUuf5iRZNzDl/jsHSI1xbpNbySxLoUQ3zC", true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountActivity",
                table: "users");

            migrationBuilder.DropColumn(
                name: "SmsUpdates",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 8, 4, 16, 40, 41, 139, DateTimeKind.Local).AddTicks(9035), "$2a$11$CF7TE0cfx7kcWn.Lrsm9c.0wlZ3qIr6U9Dj6KnL7PvCs/dWHZ4Fc." });
        }
    }
}
