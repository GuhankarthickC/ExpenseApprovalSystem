using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseApprovalSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HeadId",
                table: "Departments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "HeadId", "Name" },
                values: new object[,]
                {
                    { 1, null, "Engineering" },
                    { 2, null, "Marketing" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "Manager" },
                    { 3, "DepartmentHead" },
                    { 4, "CFO" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "ManagerId", "Name", "PasswordHash", "RoleId", "Username" },
                values: new object[,]
                {
                    { 5, 2, "david@company.com", null, "David Kim", "$2a$11$Cq4DY8R2WMvHrvj.yNVwTOwHpCcp0Dly.G0y657E6siR2Zd20qW12", 3, "david" },
                    { 7, 1, "sarah@company.com", null, "Sarah Connor", "$2a$11$Cq4DY8R2WMvHrvj.yNVwTOwHpCcp0Dly.G0y657E6siR2Zd20qW12", 3, "sarah" },
                    { 8, 1, "cfo@company.com", null, "CFO", "$2a$11$Cq4DY8R2WMvHrvj.yNVwTOwHpCcp0Dly.G0y657E6siR2Zd20qW12", 4, "cfo" },
                    { 4, 1, "alice@company.com", 7, "Alice Brown", "$2a$11$Cq4DY8R2WMvHrvj.yNVwTOwHpCcp0Dly.G0y657E6siR2Zd20qW12", 2, "alice" },
                    { 6, 2, "mike@company.com", 5, "Mike Lee", "$2a$11$Cq4DY8R2WMvHrvj.yNVwTOwHpCcp0Dly.G0y657E6siR2Zd20qW12", 2, "mike" },
                    { 1, 1, "john@company.com", 4, "John Doe", "$2a$11$Cq4DY8R2WMvHrvj.yNVwTOwHpCcp0Dly.G0y657E6siR2Zd20qW12", 1, "john" },
                    { 2, 1, "jane@company.com", 4, "Jane Smith", "$2a$11$Cq4DY8R2WMvHrvj.yNVwTOwHpCcp0Dly.G0y657E6siR2Zd20qW12", 1, "jane" },
                    { 3, 2, "bob@company.com", 6, "Bob Wilson", "$2a$11$Cq4DY8R2WMvHrvj.yNVwTOwHpCcp0Dly.G0y657E6siR2Zd20qW12", 1, "bob" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "HeadId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
