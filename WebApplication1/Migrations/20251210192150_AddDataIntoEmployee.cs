using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddDataIntoEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
