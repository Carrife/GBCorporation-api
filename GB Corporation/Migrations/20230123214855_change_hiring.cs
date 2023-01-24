using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GB_Corporation.Migrations
{
    public partial class change_hiring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantHiringDatas_Employees_LineManagerId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantHiringDatas_Employees_TeamLeaderId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantHiringDatas_SuperDictionaries_ForeignLanguageId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantHiringDatas_SuperDictionaries_ProgrammingLanguageId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropIndex(
                name: "IX_ApplicantHiringDatas_ForeignLanguageId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropIndex(
                name: "IX_ApplicantHiringDatas_ProgrammingLanguageId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropColumn(
                name: "ApplicantHiringDataIds",
                table: "HiringDatas");

            migrationBuilder.DropColumn(
                name: "ForeignLanguageId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropColumn(
                name: "ForeignLanguageResult",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropColumn(
                name: "ProgrammingLanguageId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropColumn(
                name: "ProgrammingLanguageResult",
                table: "ApplicantHiringDatas");

            migrationBuilder.AlterColumn<int>(
                name: "TeamLeaderId",
                table: "ApplicantHiringDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LineManagerId",
                table: "ApplicantHiringDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "ApplicantHiringDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ApplicantHiringDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ForeignLanguageTestId",
                table: "ApplicantHiringDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LogicTestId",
                table: "ApplicantHiringDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProgrammingTestId",
                table: "ApplicantHiringDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ApplicantForeignLanguageTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ForeignLanguageId = table.Column<int>(type: "integer", nullable: false),
                    Result = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApplicantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantForeignLanguageTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantForeignLanguageTests_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantForeignLanguageTests_SuperDictionaries_ForeignLang~",
                        column: x => x.ForeignLanguageId,
                        principalTable: "SuperDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantLogicTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Result = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApplicantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantLogicTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantLogicTests_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantProgrammingTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProgrammingLanguageId = table.Column<int>(type: "integer", nullable: false),
                    Result = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApplicantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantProgrammingTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantProgrammingTests_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantProgrammingTests_SuperDictionaries_ProgrammingLang~",
                        column: x => x.ProgrammingLanguageId,
                        principalTable: "SuperDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Login",
                table: "Employees",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_Login",
                table: "Applicants",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_ApplicantId",
                table: "ApplicantHiringDatas",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_ForeignLanguageTestId",
                table: "ApplicantHiringDatas",
                column: "ForeignLanguageTestId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_LogicTestId",
                table: "ApplicantHiringDatas",
                column: "LogicTestId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_ProgrammingTestId",
                table: "ApplicantHiringDatas",
                column: "ProgrammingTestId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantForeignLanguageTests_ApplicantId",
                table: "ApplicantForeignLanguageTests",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantForeignLanguageTests_ForeignLanguageId",
                table: "ApplicantForeignLanguageTests",
                column: "ForeignLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantLogicTests_ApplicantId",
                table: "ApplicantLogicTests",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantProgrammingTests_ApplicantId",
                table: "ApplicantProgrammingTests",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantProgrammingTests_ProgrammingLanguageId",
                table: "ApplicantProgrammingTests",
                column: "ProgrammingLanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantHiringDatas_ApplicantForeignLanguageTests_ForeignL~",
                table: "ApplicantHiringDatas",
                column: "ForeignLanguageTestId",
                principalTable: "ApplicantForeignLanguageTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantHiringDatas_ApplicantLogicTests_LogicTestId",
                table: "ApplicantHiringDatas",
                column: "LogicTestId",
                principalTable: "ApplicantLogicTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantHiringDatas_ApplicantProgrammingTests_ProgrammingT~",
                table: "ApplicantHiringDatas",
                column: "ProgrammingTestId",
                principalTable: "ApplicantProgrammingTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantHiringDatas_Applicants_ApplicantId",
                table: "ApplicantHiringDatas",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantHiringDatas_Employees_LineManagerId",
                table: "ApplicantHiringDatas",
                column: "LineManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantHiringDatas_Employees_TeamLeaderId",
                table: "ApplicantHiringDatas",
                column: "TeamLeaderId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantHiringDatas_ApplicantForeignLanguageTests_ForeignL~",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantHiringDatas_ApplicantLogicTests_LogicTestId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantHiringDatas_ApplicantProgrammingTests_ProgrammingT~",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantHiringDatas_Applicants_ApplicantId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantHiringDatas_Employees_LineManagerId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantHiringDatas_Employees_TeamLeaderId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropTable(
                name: "ApplicantForeignLanguageTests");

            migrationBuilder.DropTable(
                name: "ApplicantLogicTests");

            migrationBuilder.DropTable(
                name: "ApplicantProgrammingTests");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Login",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_Login",
                table: "Applicants");

            migrationBuilder.DropIndex(
                name: "IX_ApplicantHiringDatas_ApplicantId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropIndex(
                name: "IX_ApplicantHiringDatas_ForeignLanguageTestId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropIndex(
                name: "IX_ApplicantHiringDatas_LogicTestId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropIndex(
                name: "IX_ApplicantHiringDatas_ProgrammingTestId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropColumn(
                name: "ForeignLanguageTestId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropColumn(
                name: "LogicTestId",
                table: "ApplicantHiringDatas");

            migrationBuilder.DropColumn(
                name: "ProgrammingTestId",
                table: "ApplicantHiringDatas");

            migrationBuilder.AddColumn<List<int>>(
                name: "ApplicantHiringDataIds",
                table: "HiringDatas",
                type: "integer[]",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "TeamLeaderId",
                table: "ApplicantHiringDatas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "LineManagerId",
                table: "ApplicantHiringDatas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ForeignLanguageId",
                table: "ApplicantHiringDatas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ForeignLanguageResult",
                table: "ApplicantHiringDatas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgrammingLanguageId",
                table: "ApplicantHiringDatas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgrammingLanguageResult",
                table: "ApplicantHiringDatas",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_ForeignLanguageId",
                table: "ApplicantHiringDatas",
                column: "ForeignLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_ProgrammingLanguageId",
                table: "ApplicantHiringDatas",
                column: "ProgrammingLanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantHiringDatas_Employees_LineManagerId",
                table: "ApplicantHiringDatas",
                column: "LineManagerId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantHiringDatas_Employees_TeamLeaderId",
                table: "ApplicantHiringDatas",
                column: "TeamLeaderId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantHiringDatas_SuperDictionaries_ForeignLanguageId",
                table: "ApplicantHiringDatas",
                column: "ForeignLanguageId",
                principalTable: "SuperDictionaries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantHiringDatas_SuperDictionaries_ProgrammingLanguageId",
                table: "ApplicantHiringDatas",
                column: "ProgrammingLanguageId",
                principalTable: "SuperDictionaries",
                principalColumn: "Id");
        }
    }
}
