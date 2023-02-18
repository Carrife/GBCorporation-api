using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GB_Corporation.Migrations
{
    public partial class change_hiringData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "ApplicantHiringDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_StatusId",
                table: "ApplicantHiringDatas",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantHiringDatas_SuperDictionaries_StatusId",
                table: "ApplicantHiringDatas",
                column: "StatusId",
                principalTable: "SuperDictionaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantHiringDatas_SuperDictionaries_StatusId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropIndex(
                name: "IX_ApplicantHiringDatas_StatusId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "ApplicantHiringDatas");
        }
    }
}
