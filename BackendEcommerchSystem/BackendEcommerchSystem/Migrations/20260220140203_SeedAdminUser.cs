using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendEcommerchSystem.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "brands",
                newName: "brands",
                newSchema: "dbo");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "orders",
                type: "int",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "IsAcive", "PasswordHash", "Role" },
                values: new object[] { 5, new DateTime(2026, 2, 20, 16, 2, 0, 254, DateTimeKind.Local).AddTicks(5440), "admin@myshop.com", "System Admin", true, "$2a$11$kVuaAFKNZurObkggc6jyX.4KFeoefi3ZnDNgvjYaZOklsCDcWDuLm", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.RenameTable(
                name: "brands",
                schema: "dbo",
                newName: "brands");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20);
        }
    }
}
