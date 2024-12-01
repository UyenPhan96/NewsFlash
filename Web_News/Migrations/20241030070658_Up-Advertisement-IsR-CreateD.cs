using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_News.Migrations
{
    /// <inheritdoc />
    public partial class UpAdvertisementIsRCreateD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Advertisements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Advertisements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 50,
                column: "PublishDate",
                value: new DateTime(2024, 10, 30, 14, 6, 55, 849, DateTimeKind.Local).AddTicks(7078));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 51,
                column: "PublishDate",
                value: new DateTime(2024, 10, 30, 14, 6, 55, 849, DateTimeKind.Local).AddTicks(7080));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2024, 10, 30, 14, 6, 55, 849, DateTimeKind.Local).AddTicks(6869));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "RegistrationDate",
                value: new DateTime(2024, 10, 30, 14, 6, 55, 849, DateTimeKind.Local).AddTicks(6941));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "RegistrationDate",
                value: new DateTime(2024, 10, 30, 14, 6, 55, 849, DateTimeKind.Local).AddTicks(6956));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                column: "RegistrationDate",
                value: new DateTime(2024, 10, 30, 14, 6, 55, 849, DateTimeKind.Local).AddTicks(6969));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                column: "RegistrationDate",
                value: new DateTime(2024, 10, 30, 14, 6, 55, 849, DateTimeKind.Local).AddTicks(6982));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 6,
                column: "RegistrationDate",
                value: new DateTime(2024, 10, 30, 14, 6, 55, 849, DateTimeKind.Local).AddTicks(6994));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 7,
                column: "RegistrationDate",
                value: new DateTime(2024, 10, 30, 14, 6, 55, 849, DateTimeKind.Local).AddTicks(7007));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Advertisements");

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 50,
                column: "PublishDate",
                value: new DateTime(2024, 9, 25, 12, 29, 18, 298, DateTimeKind.Local).AddTicks(2055));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 51,
                column: "PublishDate",
                value: new DateTime(2024, 9, 25, 12, 29, 18, 298, DateTimeKind.Local).AddTicks(2057));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 25, 12, 29, 18, 298, DateTimeKind.Local).AddTicks(1898));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 25, 12, 29, 18, 298, DateTimeKind.Local).AddTicks(1920));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 25, 12, 29, 18, 298, DateTimeKind.Local).AddTicks(1934));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 25, 12, 29, 18, 298, DateTimeKind.Local).AddTicks(1948));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 25, 12, 29, 18, 298, DateTimeKind.Local).AddTicks(1961));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 6,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 25, 12, 29, 18, 298, DateTimeKind.Local).AddTicks(1973));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 7,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 25, 12, 29, 18, 298, DateTimeKind.Local).AddTicks(1986));
        }
    }
}
