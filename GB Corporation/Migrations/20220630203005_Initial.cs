using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GB_Corporation.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperDictionaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DictionaryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperDictionaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameRu = table.Column<string>(type: "text", nullable: false),
                    SurnameRu = table.Column<string>(type: "text", nullable: false),
                    PatronymicRu = table.Column<string>(type: "text", nullable: false),
                    NameEn = table.Column<string>(type: "text", nullable: false),
                    SurnameEn = table.Column<string>(type: "text", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applicants_SuperDictionaries_StatusId",
                        column: x => x.StatusId,
                        principalTable: "SuperDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameRu = table.Column<string>(type: "text", nullable: false),
                    SurnameRu = table.Column<string>(type: "text", nullable: false),
                    PatronymicRu = table.Column<string>(type: "text", nullable: false),
                    NameEn = table.Column<string>(type: "text", nullable: false),
                    SurnameEn = table.Column<string>(type: "text", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    WorkPhone = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: true),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_SuperDictionaries_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "SuperDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_SuperDictionaries_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "SuperDictionaries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_SuperDictionaries_StatusId",
                        column: x => x.StatusId,
                        principalTable: "SuperDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantHiringDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ForeignLanguageId = table.Column<int>(type: "integer", nullable: true),
                    ForeignLanguageResult = table.Column<int>(type: "integer", nullable: false),
                    ProgrammingLanguageId = table.Column<int>(type: "integer", nullable: true),
                    ProgrammingLanguageResult = table.Column<int>(type: "integer", nullable: false),
                    TeamLeaderId = table.Column<int>(type: "integer", nullable: true),
                    TeamLeaderDescription = table.Column<string>(type: "text", nullable: false),
                    LineManagerId = table.Column<int>(type: "integer", nullable: true),
                    LineManagerDescription = table.Column<string>(type: "text", nullable: false)
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
                        name: "FK_ApplicantHiringDatas_SuperDictionaries_ForeignLanguageId",
                        column: x => x.ForeignLanguageId,
                        principalTable: "SuperDictionaries",
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicantId = table.Column<int>(type: "integer", nullable: true),
                    EmployeeId = table.Column<int>(type: "integer", nullable: true),
                    ApplicantHiringDataIds = table.Column<List<int>>(type: "integer[]", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "TestCompetencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    TestResult = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCompetencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCompetencies_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantHiringDatas_ForeignLanguageId",
                table: "ApplicantHiringDatas",
                column: "ForeignLanguageId");

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
                name: "IX_Applicants_StatusId",
                table: "Applicants",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_LanguageId",
                table: "Employees",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StatusId",
                table: "Employees",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringDatas_ApplicantId",
                table: "HiringDatas",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringDatas_EmployeeId",
                table: "HiringDatas",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCompetencies_EmployeeId",
                table: "TestCompetencies",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantHiringDatas");

            migrationBuilder.DropTable(
                name: "HiringDatas");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "TestCompetencies");

            migrationBuilder.DropTable(
                name: "Applicants");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "SuperDictionaries");
        }
    }
}
