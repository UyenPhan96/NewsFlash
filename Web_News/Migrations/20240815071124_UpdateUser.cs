using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_News.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordResetCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetCodeExpiration",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "PasswordResetCode", "RegistrationDate", "ResetCodeExpiration" },
                values: new object[] { null, new DateTime(2024, 8, 15, 14, 11, 22, 324, DateTimeKind.Local).AddTicks(1446), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "PasswordResetCode", "RegistrationDate", "ResetCodeExpiration" },
                values: new object[] { null, new DateTime(2024, 8, 15, 14, 11, 22, 324, DateTimeKind.Local).AddTicks(1472), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "PasswordResetCode", "RegistrationDate", "ResetCodeExpiration" },
                values: new object[] { null, new DateTime(2024, 8, 15, 14, 11, 22, 324, DateTimeKind.Local).AddTicks(1487), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "PasswordResetCode", "RegistrationDate", "ResetCodeExpiration" },
                values: new object[] { null, new DateTime(2024, 8, 15, 14, 11, 22, 324, DateTimeKind.Local).AddTicks(1501), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "PasswordResetCode", "RegistrationDate", "ResetCodeExpiration" },
                values: new object[] { null, new DateTime(2024, 8, 15, 14, 11, 22, 324, DateTimeKind.Local).AddTicks(1514), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ResetCodeExpiration",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 13, 13, 58, 43, 851, DateTimeKind.Local).AddTicks(9087));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 13, 13, 58, 43, 851, DateTimeKind.Local).AddTicks(9115));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 13, 13, 58, 43, 851, DateTimeKind.Local).AddTicks(9131));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 13, 13, 58, 43, 851, DateTimeKind.Local).AddTicks(9145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 13, 13, 58, 43, 851, DateTimeKind.Local).AddTicks(9160));
        }
    }
}
