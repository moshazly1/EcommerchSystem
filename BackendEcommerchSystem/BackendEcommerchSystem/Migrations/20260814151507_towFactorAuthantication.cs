using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendEcommerchSystem.Migrations
{
    /// <inheritdoc />
    public partial class towFactorAuthantication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TowFactorCode",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TowFactorCodeExpiresAt",
                table: "users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash", "TowFactorCode", "TowFactorCodeExpiresAt", "TwoFactorEnabled" },
                values: new object[] { new DateTime(2026, 8, 14, 18, 15, 3, 980, DateTimeKind.Local).AddTicks(111), "$2a$11$sKH3FlBvl1OmXRXr8D.Ale6uDf/MrPXToCEUcabkS/vn8ZfSRRFt.", null, null, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TowFactorCode",
                table: "users");

            migrationBuilder.DropColumn(
                name: "TowFactorCodeExpiresAt",
                table: "users");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 8, 5, 10, 26, 47, 246, DateTimeKind.Local).AddTicks(3031), "$2a$11$swlA7RxAKxZWSyWzvtwPUuf5iRZNzDl/jsHSI1xbpNbySxLoUQ3zC" });
        }
    }
}
