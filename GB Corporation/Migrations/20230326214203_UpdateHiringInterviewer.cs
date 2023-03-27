using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GB_Corporation.Migrations
{
    public partial class UpdateHiringInterviewer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "HiringInterviewers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HiringInterviewers_PositionId",
                table: "HiringInterviewers",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_HiringInterviewers_SuperDictionaries_PositionId",
                table: "HiringInterviewers",
                column: "PositionId",
                principalTable: "SuperDictionaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HiringInterviewers_SuperDictionaries_PositionId",
                table: "HiringInterviewers");

            migrationBuilder.DropIndex(
                name: "IX_HiringInterviewers_PositionId",
                table: "HiringInterviewers");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "HiringInterviewers");
        }
    }
}
