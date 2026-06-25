using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendEcommerchSystem.Migrations
{
    /// <inheritdoc />
    public partial class addCardTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 6, 18, 11, 18, 50, 987, DateTimeKind.Local).AddTicks(3146), "$2a$11$yDvv6Dvhjc5nkjC21/hzXOlMwyPrrx7cP32ybjGPnSX0jrWDtN9jG" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 6, 15, 14, 22, 24, 641, DateTimeKind.Local).AddTicks(8059), "$2a$11$47zhttMl2TvABBtfSM1qG.oNk0tjjOv5Z08/x4WRYf.cMCCqcS5EK" });
        }
    }
}
