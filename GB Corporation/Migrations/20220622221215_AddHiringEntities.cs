using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GB_Corporation.Migrations
{
    public partial class AddHiringEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicantHiringDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ForeignLanguage = table.Column<string>(type: "TEXT", nullable: false),
                    ForeignLanguageResult = table.Column<int>(type: "INTEGER", nullable: false),
                    ProgrammingLanguageId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProgrammingLanguageResult = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamLeaderId = table.Column<int>(type: "INTEGER", nullable: true),
                    TeamLeaderDescription = table.Column<string>(type: "TEXT", nullable: false),
                    LineManagerId = table.Column<int>(type: "INTEGER", nullable: true),
                    LineManagerDescription = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantHiringDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantHiringDatas_Employees_LineManagerId",
                        column: x => x.LineManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicantHiringDatas_Employees_TeamLeaderId",
                        column: x => x.TeamLeaderId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicantHiringDatas_SuperDictionaries_ProgrammingLanguageId",
                        column: x => x.ProgrammingLanguageId,
                        principalTable: "SuperDictionaries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HiringDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApplicantId = table.Column<int>(type: "INTEGER", nullable: true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: true),
                    ApplicantHiringDataIds = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HiringDatas_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HiringDatas_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_LineManagerId",
                table: "ApplicantHiringDatas",
                column: "LineManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_ProgrammingLanguageId",
                table: "ApplicantHiringDatas",
                column: "ProgrammingLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_TeamLeaderId",
                table: "ApplicantHiringDatas",
                column: "TeamLeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringDatas_ApplicantId",
                table: "HiringDatas",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringDatas_EmployeeId",
                table: "HiringDatas",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantHiringDatas");

            migrationBuilder.DropTable(
                name: "HiringDatas");
        }
    }
}
