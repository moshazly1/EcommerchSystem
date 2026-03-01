using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendEcommerchSystem.Migrations
{
    /// <inheritdoc />
    public partial class refrechToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { new DateTime(2026, 2, 23, 16, 54, 8, 273, DateTimeKind.Local).AddTicks(9211), "$2a$11$4Cs5Dug2oU7R4KABL7MFwOycihaXTHTIJwP2KGRYw5rIL3hZ5Fr1O", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 2, 20, 16, 2, 0, 254, DateTimeKind.Local).AddTicks(5440), "$2a$11$kVuaAFKNZurObkggc6jyX.4KFeoefi3ZnDNgvjYaZOklsCDcWDuLm" });
        }
    }
}
