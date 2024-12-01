using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web_News.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Describe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "Describe", "NameRole" },
                values: new object[,]
                {
                    { 1, "Administrator Role", "Admin" },
                    { 2, "Customer Role", "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "Email", "Name", "Password", "Phone", "RegistrationDate", "UserName" },
                values: new object[,]
                {
                    { 1, "123 Admin St", "admin@gmail.com", "Administrator", "admin", "123456789", new DateTime(2024, 8, 12, 15, 24, 36, 325, DateTimeKind.Local).AddTicks(9300), "admin" },
                    { 2, "456 User Ave", "hngoctro@gmail.com", "Huỳnh Ngọc Trợ", "1234qwer", "987654321", new DateTime(2024, 8, 12, 15, 24, 36, 325, DateTimeKind.Local).AddTicks(9309), "NgocTro" },
                    { 3, "456 User Ave", "phucbin366@gmail.com", "Trần Văn Phúc", "1234qwer", "987654321", new DateTime(2024, 8, 12, 15, 24, 36, 325, DateTimeKind.Local).AddTicks(9311), "VanPhuc" },
                    { 4, "456 User Ave", "caothiphuongvy27@gmail.com", "Cao Thị Phương Vy", "1234qwer", "987654321", new DateTime(2024, 8, 12, 15, 24, 36, 325, DateTimeKind.Local).AddTicks(9312), "PhuongVy" },
                    { 5, "456 User Ave", "nguyenngocquy182752@gmail.com", "Nguyễn Thị Ngọc Quý", "1234qwer", "987654321", new DateTime(2024, 8, 12, 15, 24, 36, 325, DateTimeKind.Local).AddTicks(9314), "NgocQuy" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
