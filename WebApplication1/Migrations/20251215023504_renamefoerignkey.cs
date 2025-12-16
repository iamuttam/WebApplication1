using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class renamefoerignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_Students_Department",
                table: "employees");

            migrationBuilder.AddForeignKey(
                name: "Fk_Employee_Department",
                table: "employees",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_Employee_Department",
                table: "employees");

            migrationBuilder.AddForeignKey(
                name: "Fk_Students_Department",
                table: "employees",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId");
        }
    }
}
