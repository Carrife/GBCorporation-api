using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GB_Corporation.Migrations
{
    public partial class changeEmployeeDeleteSomeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTestRequest",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Employees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "Employees");

            migrationBuilder.AddColumn<bool>(
                name: "IsTestRequest",
                table: "Employees",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhoto",
                table: "Employees",
                type: "TEXT",
                nullable: true);
        }
    }
}
