using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GB_Corporation.Migrations
{
    public partial class UpdateHiringModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HiringDatas_Applicants_ApplicantId",
                table: "HiringDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_HiringDatas_Employees_EmployeeId",
                table: "HiringDatas");

            migrationBuilder.DropTable(
                name: "ApplicantHiringDatas");

            migrationBuilder.DropIndex(
                name: "IX_HiringDatas_EmployeeId",
                table: "HiringDatas");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "HiringDatas");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicantId",
                table: "HiringDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "HiringDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "HiringDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "HiringDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ApplicantEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicantId = table.Column<int>(type: "integer", nullable: true),
                    EmployeeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantEmployees_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicantEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HiringInterviewers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InterviewerId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    HiringDataId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringInterviewers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HiringInterviewers_Employees_InterviewerId",
                        column: x => x.InterviewerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HiringInterviewers_HiringDatas_HiringDataId",
                        column: x => x.HiringDataId,
                        principalTable: "HiringDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HiringTestDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ForeignLanguageTestId = table.Column<int>(type: "integer", nullable: true),
                    LogicTestId = table.Column<int>(type: "integer", nullable: true),
                    ProgrammingTestId = table.Column<int>(type: "integer", nullable: true),
                    HiringDataId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringTestDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HiringTestDatas_ApplicantForeignLanguageTests_ForeignLangua~",
                        column: x => x.ForeignLanguageTestId,
                        principalTable: "ApplicantForeignLanguageTests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HiringTestDatas_ApplicantLogicTests_LogicTestId",
                        column: x => x.LogicTestId,
                        principalTable: "ApplicantLogicTests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HiringTestDatas_ApplicantProgrammingTests_ProgrammingTestId",
                        column: x => x.ProgrammingTestId,
                        principalTable: "ApplicantProgrammingTests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HiringTestDatas_HiringDatas_HiringDataId",
                        column: x => x.HiringDataId,
                        principalTable: "HiringDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HiringDatas_PositionId",
                table: "HiringDatas",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringDatas_StatusId",
                table: "HiringDatas",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantEmployees_ApplicantId",
                table: "ApplicantEmployees",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantEmployees_EmployeeId",
                table: "ApplicantEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringInterviewers_HiringDataId",
                table: "HiringInterviewers",
                column: "HiringDataId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringInterviewers_InterviewerId",
                table: "HiringInterviewers",
                column: "InterviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringTestDatas_ForeignLanguageTestId",
                table: "HiringTestDatas",
                column: "ForeignLanguageTestId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringTestDatas_HiringDataId",
                table: "HiringTestDatas",
                column: "HiringDataId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringTestDatas_LogicTestId",
                table: "HiringTestDatas",
                column: "LogicTestId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringTestDatas_ProgrammingTestId",
                table: "HiringTestDatas",
                column: "ProgrammingTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_HiringDatas_Applicants_ApplicantId",
                table: "HiringDatas",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HiringDatas_SuperDictionaries_PositionId",
                table: "HiringDatas",
                column: "PositionId",
                principalTable: "SuperDictionaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HiringDatas_SuperDictionaries_StatusId",
                table: "HiringDatas",
                column: "StatusId",
                principalTable: "SuperDictionaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HiringDatas_Applicants_ApplicantId",
                table: "HiringDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_HiringDatas_SuperDictionaries_PositionId",
                table: "HiringDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_HiringDatas_SuperDictionaries_StatusId",
                table: "HiringDatas");

            migrationBuilder.DropTable(
                name: "ApplicantEmployees");

            migrationBuilder.DropTable(
                name: "HiringInterviewers");

            migrationBuilder.DropTable(
                name: "HiringTestDatas");

            migrationBuilder.DropIndex(
                name: "IX_HiringDatas_PositionId",
                table: "HiringDatas");

            migrationBuilder.DropIndex(
                name: "IX_HiringDatas_StatusId",
                table: "HiringDatas");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "HiringDatas");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "HiringDatas");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "HiringDatas");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicantId",
                table: "HiringDatas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "HiringDatas",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicantHiringDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicantId = table.Column<int>(type: "integer", nullable: false),
                    ForeignLanguageTestId = table.Column<int>(type: "integer", nullable: false),
                    LineManagerId = table.Column<int>(type: "integer", nullable: false),
                    LogicTestId = table.Column<int>(type: "integer", nullable: false),
                    ProgrammingTestId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    TeamLeaderId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LineManagerDescription = table.Column<string>(type: "text", nullable: true),
                    TeamLeaderDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantHiringDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantHiringDatas_ApplicantForeignLanguageTests_ForeignL~",
                        column: x => x.ForeignLanguageTestId,
                        principalTable: "ApplicantForeignLanguageTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantHiringDatas_ApplicantLogicTests_LogicTestId",
                        column: x => x.LogicTestId,
                        principalTable: "ApplicantLogicTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantHiringDatas_ApplicantProgrammingTests_ProgrammingT~",
                        column: x => x.ProgrammingTestId,
                        principalTable: "ApplicantProgrammingTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantHiringDatas_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantHiringDatas_Employees_LineManagerId",
                        column: x => x.LineManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantHiringDatas_Employees_TeamLeaderId",
                        column: x => x.TeamLeaderId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantHiringDatas_SuperDictionaries_StatusId",
                        column: x => x.StatusId,
                        principalTable: "SuperDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HiringDatas_EmployeeId",
                table: "HiringDatas",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_ApplicantId",
                table: "ApplicantHiringDatas",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_ForeignLanguageTestId",
                table: "ApplicantHiringDatas",
                column: "ForeignLanguageTestId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_LineManagerId",
                table: "ApplicantHiringDatas",
                column: "LineManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_LogicTestId",
                table: "ApplicantHiringDatas",
                column: "LogicTestId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_ProgrammingTestId",
                table: "ApplicantHiringDatas",
                column: "ProgrammingTestId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_StatusId",
                table: "ApplicantHiringDatas",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_TeamLeaderId",
                table: "ApplicantHiringDatas",
                column: "TeamLeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_HiringDatas_Applicants_ApplicantId",
                table: "HiringDatas",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HiringDatas_Employees_EmployeeId",
                table: "HiringDatas",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
