using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_News.Migrations
{
    /// <inheritdoc />
    public partial class PasswordHasher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Address", "Password", "Phone", "RegistrationDate" },
                values: new object[] { null, "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", null, new DateTime(2024, 8, 13, 13, 58, 43, 851, DateTimeKind.Local).AddTicks(9087) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "Address", "Password", "Phone", "RegistrationDate" },
                values: new object[] { null, "71EwYhTZpjYe4dW0UubSu3DcfruFv54Cw9R0f7V9a+w=", null, new DateTime(2024, 8, 13, 13, 58, 43, 851, DateTimeKind.Local).AddTicks(9115) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "Address", "Password", "Phone", "RegistrationDate" },
                values: new object[] { null, "71EwYhTZpjYe4dW0UubSu3DcfruFv54Cw9R0f7V9a+w=", null, new DateTime(2024, 8, 13, 13, 58, 43, 851, DateTimeKind.Local).AddTicks(9131) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "Address", "Password", "Phone", "RegistrationDate" },
                values: new object[] { null, "71EwYhTZpjYe4dW0UubSu3DcfruFv54Cw9R0f7V9a+w=", null, new DateTime(2024, 8, 13, 13, 58, 43, 851, DateTimeKind.Local).AddTicks(9145) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "Address", "Password", "Phone", "RegistrationDate" },
                values: new object[] { null, "71EwYhTZpjYe4dW0UubSu3DcfruFv54Cw9R0f7V9a+w=", null, new DateTime(2024, 8, 13, 13, 58, 43, 851, DateTimeKind.Local).AddTicks(9160) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Address", "Password", "Phone", "RegistrationDate" },
                values: new object[] { "123 Admin St", "admin", "123456789", new DateTime(2024, 8, 12, 15, 24, 36, 325, DateTimeKind.Local).AddTicks(9300) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "Address", "Password", "Phone", "RegistrationDate" },
                values: new object[] { "456 User Ave", "1234qwer", "987654321", new DateTime(2024, 8, 12, 15, 24, 36, 325, DateTimeKind.Local).AddTicks(9309) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "Address", "Password", "Phone", "RegistrationDate" },
                values: new object[] { "456 User Ave", "1234qwer", "987654321", new DateTime(2024, 8, 12, 15, 24, 36, 325, DateTimeKind.Local).AddTicks(9311) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "Address", "Password", "Phone", "RegistrationDate" },
                values: new object[] { "456 User Ave", "1234qwer", "987654321", new DateTime(2024, 8, 12, 15, 24, 36, 325, DateTimeKind.Local).AddTicks(9312) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "Address", "Password", "Phone", "RegistrationDate" },
                values: new object[] { "456 User Ave", "1234qwer", "987654321", new DateTime(2024, 8, 12, 15, 24, 36, 325, DateTimeKind.Local).AddTicks(9314) });
        }
    }
}
