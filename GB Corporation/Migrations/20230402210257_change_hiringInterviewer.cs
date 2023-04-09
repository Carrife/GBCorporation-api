using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GB_Corporation.Migrations
{
    public partial class change_hiringInterviewer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HiringInterviewers_SuperDictionaries_PositionId",
                table: "HiringInterviewers");

            migrationBuilder.AddForeignKey(
                name: "FK_HiringInterviewers_Roles_PositionId",
                table: "HiringInterviewers",
                column: "PositionId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HiringInterviewers_Roles_PositionId",
                table: "HiringInterviewers");

            migrationBuilder.AddForeignKey(
                name: "FK_HiringInterviewers_SuperDictionaries_PositionId",
                table: "HiringInterviewers",
                column: "PositionId",
                principalTable: "SuperDictionaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
