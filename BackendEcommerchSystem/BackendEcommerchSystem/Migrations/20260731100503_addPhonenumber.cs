using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendEcommerchSystem.Migrations
{
    /// <inheritdoc />
    public partial class addPhonenumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash", "PhoneNumber" },
                values: new object[] { new DateTime(2026, 7, 31, 13, 4, 59, 409, DateTimeKind.Local).AddTicks(1460), "$2a$11$W.NPXl4Sh9MQEtQb2ZlabO6Uy8M5Dx6WL.4OYd9mHWmnlCttMuUMe", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 4, 10, 50, 30, 628, DateTimeKind.Local).AddTicks(4515), "$2a$11$a1YQ/osp5WiqS6vMMJ3bZOXVHFPyx2QIZhHvDt8V15VrtMR8L4UVC" });
        }
    }
}
