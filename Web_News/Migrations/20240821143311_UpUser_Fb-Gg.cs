using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_News.Migrations
{
    /// <inheritdoc />
    public partial class UpUser_FbGg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Optional",
                table: "Users",
                newName: "IdGoogle");

            migrationBuilder.AddColumn<string>(
                name: "IdFacebook",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "IdFacebook", "RegistrationDate" },
                values: new object[] { null, new DateTime(2024, 8, 21, 21, 33, 9, 65, DateTimeKind.Local).AddTicks(9711) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "IdFacebook", "RegistrationDate" },
                values: new object[] { null, new DateTime(2024, 8, 21, 21, 33, 9, 65, DateTimeKind.Local).AddTicks(9736) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "IdFacebook", "RegistrationDate" },
                values: new object[] { null, new DateTime(2024, 8, 21, 21, 33, 9, 65, DateTimeKind.Local).AddTicks(9785) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "IdFacebook", "RegistrationDate" },
                values: new object[] { null, new DateTime(2024, 8, 21, 21, 33, 9, 65, DateTimeKind.Local).AddTicks(9798) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "IdFacebook", "RegistrationDate" },
                values: new object[] { null, new DateTime(2024, 8, 21, 21, 33, 9, 65, DateTimeKind.Local).AddTicks(9812) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdFacebook",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "IdGoogle",
                table: "Users",
                newName: "Optional");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 20, 9, 38, 28, 568, DateTimeKind.Local).AddTicks(9086));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 20, 9, 38, 28, 568, DateTimeKind.Local).AddTicks(9111));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 20, 9, 38, 28, 568, DateTimeKind.Local).AddTicks(9126));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 20, 9, 38, 28, 568, DateTimeKind.Local).AddTicks(9140));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                column: "RegistrationDate",
                value: new DateTime(2024, 8, 20, 9, 38, 28, 568, DateTimeKind.Local).AddTicks(9154));
        }
    }
}
