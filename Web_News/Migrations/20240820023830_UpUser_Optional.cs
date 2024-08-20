using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_News.Migrations
{
    /// <inheritdoc />
    public partial class UpUser_Optional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Optional",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Optional", "RegistrationDate" },
                values: new object[] { null, new DateTime(2024, 8, 20, 9, 38, 28, 568, DateTimeKind.Local).AddTicks(9086) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "Optional", "RegistrationDate" },
                values: new object[] { null, new DateTime(2024, 8, 20, 9, 38, 28, 568, DateTimeKind.Local).AddTicks(9111) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "Optional", "RegistrationDate" },
                values: new object[] { null, new DateTime(2024, 8, 20, 9, 38, 28, 568, DateTimeKind.Local).AddTicks(9126) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "Optional", "RegistrationDate" },
                values: new object[] { null, new DateTime(2024, 8, 20, 9, 38, 28, 568, DateTimeKind.Local).AddTicks(9140) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "Optional", "RegistrationDate" },
                values: new object[] { null, new DateTime(2024, 8, 20, 9, 38, 28, 568, DateTimeKind.Local).AddTicks(9154) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Optional",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 15, 14, 11, 22, 324, DateTimeKind.Local).AddTicks(1446));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 15, 14, 11, 22, 324, DateTimeKind.Local).AddTicks(1472));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 15, 14, 11, 22, 324, DateTimeKind.Local).AddTicks(1487));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 15, 14, 11, 22, 324, DateTimeKind.Local).AddTicks(1501));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 15, 14, 11, 22, 324, DateTimeKind.Local).AddTicks(1514));
        }
    }
}
