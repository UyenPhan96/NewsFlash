using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_News.Migrations
{
    /// <inheritdoc />
    public partial class Up_News_Category_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "News",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 1, 20, 35, 10, 113, DateTimeKind.Local).AddTicks(810));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 1, 20, 35, 10, 113, DateTimeKind.Local).AddTicks(835));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 1, 20, 35, 10, 113, DateTimeKind.Local).AddTicks(850));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 1, 20, 35, 10, 113, DateTimeKind.Local).AddTicks(863));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 1, 20, 35, 10, 113, DateTimeKind.Local).AddTicks(876));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 30, 11, 17, 28, 323, DateTimeKind.Local).AddTicks(6861));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 30, 11, 17, 28, 323, DateTimeKind.Local).AddTicks(6902));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 30, 11, 17, 28, 323, DateTimeKind.Local).AddTicks(6917));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 30, 11, 17, 28, 323, DateTimeKind.Local).AddTicks(6931));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 30, 11, 17, 28, 323, DateTimeKind.Local).AddTicks(6945));
        }
    }
}
