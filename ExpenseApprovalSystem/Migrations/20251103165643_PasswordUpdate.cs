using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseApprovalSystem.Migrations
{
    /// <inheritdoc />
    public partial class PasswordUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "password123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "password123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "password123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "password123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "PasswordHash",
                value: "password123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "PasswordHash",
                value: "password123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "PasswordHash",
                value: "password123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                column: "PasswordHash",
                value: "password123");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$xKYVHbpYq2Le1J6Q3N9UHeMm2YIZhLqYOvXOIQN5K9qIf3RJ6.8Gy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$xKYVHbpYq2Le1J6Q3N9UHeMm2YIZhLqYOvXOIQN5K9qIf3RJ6.8Gy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$xKYVHbpYq2Le1J6Q3N9UHeMm2YIZhLqYOvXOIQN5K9qIf3RJ6.8Gy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$xKYVHbpYq2Le1J6Q3N9UHeMm2YIZhLqYOvXOIQN5K9qIf3RJ6.8Gy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "PasswordHash",
                value: "$2a$11$xKYVHbpYq2Le1J6Q3N9UHeMm2YIZhLqYOvXOIQN5K9qIf3RJ6.8Gy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "PasswordHash",
                value: "$2a$11$xKYVHbpYq2Le1J6Q3N9UHeMm2YIZhLqYOvXOIQN5K9qIf3RJ6.8Gy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "PasswordHash",
                value: "$2a$11$xKYVHbpYq2Le1J6Q3N9UHeMm2YIZhLqYOvXOIQN5K9qIf3RJ6.8Gy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                column: "PasswordHash",
                value: "$2a$11$xKYVHbpYq2Le1J6Q3N9UHeMm2YIZhLqYOvXOIQN5K9qIf3RJ6.8Gy");
        }
    }
}
