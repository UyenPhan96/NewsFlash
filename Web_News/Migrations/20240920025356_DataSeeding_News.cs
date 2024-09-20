using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web_News.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding_News : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewsId", "ApprovalStatus", "Content", "CreatedByUserId", "Image", "PublishDate", "RejectionReason", "Status", "Title" },
                values: new object[,]
                {
                    { 50, 1, "Công nghệ mới đang được phát triển toàn cầu.", 1, "ct.jpeg", new DateTime(2024, 9, 20, 9, 53, 56, 97, DateTimeKind.Local).AddTicks(834), null, true, "Công nghệ mới" },
                    { 51, 1, "Sức khỏe cộng đồng Sức khỏe cộng đồng.", 1, "ct.jpeg", new DateTime(2024, 9, 20, 9, 53, 56, 97, DateTimeKind.Local).AddTicks(837), null, true, "Sức khỏe cộng đồng" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 20, 9, 53, 56, 97, DateTimeKind.Local).AddTicks(699));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 20, 9, 53, 56, 97, DateTimeKind.Local).AddTicks(724));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 20, 9, 53, 56, 97, DateTimeKind.Local).AddTicks(739));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 20, 9, 53, 56, 97, DateTimeKind.Local).AddTicks(753));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 20, 9, 53, 56, 97, DateTimeKind.Local).AddTicks(766));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 51);

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewsId", "ApprovalStatus", "Content", "CreatedByUserId", "Image", "PublishDate", "RejectionReason", "Status", "Title" },
                values: new object[,]
                {
                    { -2, 1, "Sức khỏe cộng đồng Sức khỏe cộng đồng.", 4, "ct.jpeg", new DateTime(2024, 9, 20, 9, 47, 8, 116, DateTimeKind.Local).AddTicks(1968), null, true, "Sức khỏe cộng đồng" },
                    { -1, 1, "Công nghệ mới đang được phát triển toàn cầu.", 2, "ct.jpeg", new DateTime(2024, 9, 20, 9, 47, 8, 116, DateTimeKind.Local).AddTicks(1966), null, true, "Công nghệ mới" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 20, 9, 47, 8, 116, DateTimeKind.Local).AddTicks(1780));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 20, 9, 47, 8, 116, DateTimeKind.Local).AddTicks(1805));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 20, 9, 47, 8, 116, DateTimeKind.Local).AddTicks(1822));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 20, 9, 47, 8, 116, DateTimeKind.Local).AddTicks(1838));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 20, 9, 47, 8, 116, DateTimeKind.Local).AddTicks(1851));
        }
    }
}
