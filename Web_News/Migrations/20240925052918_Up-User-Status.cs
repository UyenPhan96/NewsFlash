using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_News.Migrations
{
    /// <inheritdoc />
    public partial class UpUserStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AccountStatus",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                columns: new[] { "AccountStatus", "IsDeleted", "RegistrationDate" },
                values: new object[] { false, false, new DateTime(2024, 9, 25, 12, 29, 18, 298, DateTimeKind.Local).AddTicks(1898) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "AccountStatus", "IsDeleted", "RegistrationDate" },
                values: new object[] { false, false, new DateTime(2024, 9, 25, 12, 29, 18, 298, DateTimeKind.Local).AddTicks(1920) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "AccountStatus", "IsDeleted", "RegistrationDate" },
                values: new object[] { false, false, new DateTime(2024, 9, 25, 12, 29, 18, 298, DateTimeKind.Local).AddTicks(1934) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "AccountStatus", "IsDeleted", "RegistrationDate" },
                values: new object[] { false, false, new DateTime(2024, 9, 25, 12, 29, 18, 298, DateTimeKind.Local).AddTicks(1948) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "AccountStatus", "IsDeleted", "RegistrationDate" },
                values: new object[] { false, false, new DateTime(2024, 9, 25, 12, 29, 18, 298, DateTimeKind.Local).AddTicks(1961) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 6,
                columns: new[] { "AccountStatus", "IsDeleted", "RegistrationDate" },
                values: new object[] { false, false, new DateTime(2024, 9, 25, 12, 29, 18, 298, DateTimeKind.Local).AddTicks(1973) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 7,
                columns: new[] { "AccountStatus", "IsDeleted", "RegistrationDate" },
                values: new object[] { false, false, new DateTime(2024, 9, 25, 12, 29, 18, 298, DateTimeKind.Local).AddTicks(1986) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountStatus",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 50,
                column: "PublishDate",
                value: new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4504));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 51,
                column: "PublishDate",
                value: new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4507));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4334));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4365));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4379));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4393));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4406));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 6,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4419));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 7,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4432));
        }
    }
}
