using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GB_Corporation.Migrations
{
    public partial class Change_test_competencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "TestCompetencies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TestCompetencies_StatusId",
                table: "TestCompetencies",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestCompetencies_SuperDictionaries_StatusId",
                table: "TestCompetencies",
                column: "StatusId",
                principalTable: "SuperDictionaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestCompetencies_SuperDictionaries_StatusId",
                table: "TestCompetencies");

            migrationBuilder.DropIndex(
                name: "IX_TestCompetencies_StatusId",
                table: "TestCompetencies");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "TestCompetencies");
        }
    }
}
