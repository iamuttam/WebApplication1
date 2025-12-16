using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "EmployeeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "EmployeeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "EmployeeId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Department",
                table: "employees");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "DepartmentId", "DepartmentName", "Description" },
                values: new object[,]
                {
                    { 1, "Uttam", "sdhfgyefywefewydfwtdfwtdfwtdrwdf" },
                    { 2, "Development", "sdhfgyefywefewydfwtdfwtdfwtdrwdf" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_employees_DepartmentId",
                table: "employees",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "Fk_Students_Department",
                table: "employees",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_Students_Department",
                table: "employees");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropIndex(
                name: "IX_employees_DepartmentId",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "employees");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "EmployeeId", "DateofJoining", "Department", "Description", "Email", "EmployeeAge", "EmployeeName", "Experience", "Gender" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Development", "sdhfgyefywefewydfwtdfwtdfwtdrwdf", "uttam@gmail.com", 29, "Uttam", "5 yrs.", "M" },
                    { 2, new DateTime(2021, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Development", "sdhfgyefywefewydfwtdfwtdfwtdrwdf", "uttam@gmail.com", 29, "Uttam Kumar", "5 yrs.", "M" },
                    { 3, new DateTime(2021, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Development", "sdhfgyefywefewydfwtdfwtdfwtdrwdf", "uttam@gmail.com", 29, "Uttam singh", "5 yrs.", "M" }
                });
        }
    }
}
