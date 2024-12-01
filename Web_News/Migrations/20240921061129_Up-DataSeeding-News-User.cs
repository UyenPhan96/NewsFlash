using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web_News.Migrations
{
    /// <inheritdoc />
    public partial class UpDataSeedingNewsUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Describe", "NameCategory", "ParentCategoryId" },
                values: new object[,]
                {
                    { 50, "Latest technology news", "Công nghệ", null },
                    { 51, "Health and wellness tips", "Sức khỏe", null }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewsId", "ApprovalStatus", "Content", "CreatedByUserId", "Image", "PublishDate", "RejectionReason", "Status", "Title" },
                values: new object[,]
                {
                    { 50, 1, "Công nghệ mới đang được phát triển toàn cầu.", 1, "ct.jpeg", new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4504), null, true, "Công nghệ mới" },
                    { 51, 1, "Sức khỏe cộng đồng Sức khỏe cộng đồng.", 1, "ct.jpeg", new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4507), null, true, "Sức khỏe cộng đồng" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 2,
                column: "Describe",
                value: "Vai trò người dùng trải nghiệm");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "Describe", "NameRole" },
                values: new object[,]
                {
                    { 3, "Vai trò người viết tin tức", "Reporter" },
                    { 4, "Vai trò người kiểm duyệt tin tức", "Editor" }
                });

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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "Email", "IdFacebook", "IdGoogle", "Name", "Password", "PasswordResetCode", "Phone", "RegistrationDate", "ResetCodeExpiration", "UserName" },
                values: new object[,]
                {
                    { 6, null, "reporter@gmail.com", null, null, "Nguyễn Văn Ánh", "71EwYhTZpjYe4dW0UubSu3DcfruFv54Cw9R0f7V9a+w=", null, null, new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4419), null, "reporter" },
                    { 7, null, "editor@gmail.com", null, null, "Cao Văn Lãnh", "71EwYhTZpjYe4dW0UubSu3DcfruFv54Cw9R0f7V9a+w=", null, null, new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4432), null, "editor" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 3, 2 },
                    { 3, 4 },
                    { 3, 6 },
                    { 4, 7 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 7 });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 2,
                column: "Describe",
                value: "Customer Role");

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 2, 2 },
                    { 2, 4 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 17, 16, 17, 32, 972, DateTimeKind.Local).AddTicks(1208));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 17, 16, 17, 32, 972, DateTimeKind.Local).AddTicks(1239));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 17, 16, 17, 32, 972, DateTimeKind.Local).AddTicks(1254));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 17, 16, 17, 32, 972, DateTimeKind.Local).AddTicks(1310));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                column: "RegistrationDate",
                value: new DateTime(2024, 9, 17, 16, 17, 32, 972, DateTimeKind.Local).AddTicks(1326));
        }
    }
}
