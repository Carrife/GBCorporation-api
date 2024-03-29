﻿// <auto-generated />
using System;
using GB_Corporation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GB_Corporation.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230326214203_UpdateHiringInterviewer")]
    partial class UpdateHiringInterviewer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GB_Corporation.Models.Applicant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NameRu")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PatronymicRu")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<string>("SurnameEn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SurnameRu")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("StatusId");

                    b.ToTable("Applicants");
                });

            modelBuilder.Entity("GB_Corporation.Models.ApplicantEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ApplicantId")
                        .HasColumnType("integer");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("ApplicantEmployees");
                });

            modelBuilder.Entity("GB_Corporation.Models.ApplicantForeignLanguageTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ApplicantId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ForeignLanguageId")
                        .HasColumnType("integer");

                    b.Property<int>("Result")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("ForeignLanguageId");

                    b.ToTable("ApplicantForeignLanguageTests");
                });

            modelBuilder.Entity("GB_Corporation.Models.ApplicantLogicTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ApplicantId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Result")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.ToTable("ApplicantLogicTests");
                });

            modelBuilder.Entity("GB_Corporation.Models.ApplicantProgrammingTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ApplicantId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ProgrammingLanguageId")
                        .HasColumnType("integer");

                    b.Property<int>("Result")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("ProgrammingLanguageId");

                    b.ToTable("ApplicantProgrammingTests");
                });

            modelBuilder.Entity("GB_Corporation.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NameRu")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PatronymicRu")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<string>("SurnameEn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SurnameRu")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WorkPhone")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("LanguageId");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("StatusId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("GB_Corporation.Models.HiringData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ApplicantId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("PositionId");

                    b.HasIndex("StatusId");

                    b.ToTable("HiringDatas");
                });

            modelBuilder.Entity("GB_Corporation.Models.HiringInterviewer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("HiringDataId")
                        .HasColumnType("integer");

                    b.Property<int>("InterviewerId")
                        .HasColumnType("integer");

                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HiringDataId");

                    b.HasIndex("InterviewerId");

                    b.HasIndex("PositionId");

                    b.ToTable("HiringInterviewers");
                });

            modelBuilder.Entity("GB_Corporation.Models.HiringTestData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ForeignLanguageTestId")
                        .HasColumnType("integer");

                    b.Property<int>("HiringDataId")
                        .HasColumnType("integer");

                    b.Property<int?>("LogicTestId")
                        .HasColumnType("integer");

                    b.Property<int?>("ProgrammingTestId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ForeignLanguageTestId");

                    b.HasIndex("HiringDataId");

                    b.HasIndex("LogicTestId");

                    b.HasIndex("ProgrammingTestId");

                    b.ToTable("HiringTestDatas");
                });

            modelBuilder.Entity("GB_Corporation.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("GB_Corporation.Models.SuperDictionary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DictionaryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SuperDictionaries");
                });

            modelBuilder.Entity("GB_Corporation.Models.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("GB_Corporation.Models.TestCompetencies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<int>("TestResult")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("TestCompetencies");
                });

            modelBuilder.Entity("GB_Corporation.Models.Applicant", b =>
                {
                    b.HasOne("GB_Corporation.Models.SuperDictionary", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("GB_Corporation.Models.ApplicantEmployee", b =>
                {
                    b.HasOne("GB_Corporation.Models.Applicant", "Applicant")
                        .WithMany()
                        .HasForeignKey("ApplicantId");

                    b.HasOne("GB_Corporation.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Applicant");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("GB_Corporation.Models.ApplicantForeignLanguageTest", b =>
                {
                    b.HasOne("GB_Corporation.Models.Applicant", "Applicant")
                        .WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GB_Corporation.Models.SuperDictionary", "ForeignLanguage")
                        .WithMany()
                        .HasForeignKey("ForeignLanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");

                    b.Navigation("ForeignLanguage");
                });

            modelBuilder.Entity("GB_Corporation.Models.ApplicantLogicTest", b =>
                {
                    b.HasOne("GB_Corporation.Models.Applicant", "Applicant")
                        .WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("GB_Corporation.Models.ApplicantProgrammingTest", b =>
                {
                    b.HasOne("GB_Corporation.Models.Applicant", "Applicant")
                        .WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GB_Corporation.Models.SuperDictionary", "ProgrammingLanguage")
                        .WithMany()
                        .HasForeignKey("ProgrammingLanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");

                    b.Navigation("ProgrammingLanguage");
                });

            modelBuilder.Entity("GB_Corporation.Models.Employee", b =>
                {
                    b.HasOne("GB_Corporation.Models.SuperDictionary", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GB_Corporation.Models.SuperDictionary", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");

                    b.HasOne("GB_Corporation.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GB_Corporation.Models.SuperDictionary", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Language");

                    b.Navigation("Role");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("GB_Corporation.Models.HiringData", b =>
                {
                    b.HasOne("GB_Corporation.Models.Applicant", "Applicant")
                        .WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GB_Corporation.Models.SuperDictionary", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GB_Corporation.Models.SuperDictionary", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");

                    b.Navigation("Position");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("GB_Corporation.Models.HiringInterviewer", b =>
                {
                    b.HasOne("GB_Corporation.Models.HiringData", "HiringData")
                        .WithMany()
                        .HasForeignKey("HiringDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GB_Corporation.Models.Employee", "Interviewer")
                        .WithMany()
                        .HasForeignKey("InterviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GB_Corporation.Models.SuperDictionary", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HiringData");

                    b.Navigation("Interviewer");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("GB_Corporation.Models.HiringTestData", b =>
                {
                    b.HasOne("GB_Corporation.Models.ApplicantForeignLanguageTest", "ForeignLanguageTest")
                        .WithMany()
                        .HasForeignKey("ForeignLanguageTestId");

                    b.HasOne("GB_Corporation.Models.HiringData", "HiringData")
                        .WithMany()
                        .HasForeignKey("HiringDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GB_Corporation.Models.ApplicantLogicTest", "LogicTest")
                        .WithMany()
                        .HasForeignKey("LogicTestId");

                    b.HasOne("GB_Corporation.Models.ApplicantProgrammingTest", "ProgrammingTest")
                        .WithMany()
                        .HasForeignKey("ProgrammingTestId");

                    b.Navigation("ForeignLanguageTest");

                    b.Navigation("HiringData");

                    b.Navigation("LogicTest");

                    b.Navigation("ProgrammingTest");
                });

            modelBuilder.Entity("GB_Corporation.Models.TestCompetencies", b =>
                {
                    b.HasOne("GB_Corporation.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
