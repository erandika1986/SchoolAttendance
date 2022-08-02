using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendance.Infrastructure.Migrations
{
    public partial class JobApp00001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AcademicYear",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCurrentYear = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYear", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AssessmentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClassSubjectStudent",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeAllocatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSubjectStudent", x => new { x.ClassId, x.SubjectId, x.StudentId });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonLectureContentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IconPath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonLectureContentType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContactNo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeZoneId = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmationCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastLoggedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LevelHeadId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grade_User",
                        column: x => x.LevelHeadId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Medium = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartmentHeadId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "((1))"),
                    IsBasketSubject = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ParentSubjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subject_Subject",
                        column: x => x.ParentSubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subject_User",
                        column: x => x.DepartmentHeadId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    AssignedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRole_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClassTeacherId = table.Column<int>(type: "int", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    AcademicYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Class_AcademicYear",
                        column: x => x.AcademicYear,
                        principalTable: "AcademicYear",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Class_Grade",
                        column: x => x.GradeId,
                        principalTable: "Grade",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Class_User",
                        column: x => x.ClassTeacherId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Assessment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AcademicYearId = table.Column<int>(type: "int", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    AssessmentTypeId = table.Column<int>(type: "int", nullable: false),
                    Statue = table.Column<int>(type: "int", nullable: false),
                    VersionNo = table.Column<int>(type: "int", nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: true),
                    PublishedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CompletedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assessment_AcademicYear",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessment_AssessmentType",
                        column: x => x.AssessmentTypeId,
                        principalTable: "AssessmentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessment_Grade",
                        column: x => x.GradeId,
                        principalTable: "Grade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessment_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessment_User",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assessment_User1",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assessment_User2",
                        column: x => x.ApprovedBy,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GradeSubject",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeSubject", x => new { x.SubjectId, x.GradeId });
                    table.ForeignKey(
                        name: "FK_GradeSubject_Grade",
                        column: x => x.GradeId,
                        principalTable: "Grade",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GradeSubject_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LessonIntroduction = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Duration = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    CompetencyLevel = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TeachingAids = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LessonOwnerId = table.Column<int>(type: "int", nullable: false),
                    AcademicYearId = table.Column<int>(type: "int", nullable: true),
                    GradeId = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TeachingProcess = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HasLessonTest = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lesson_AcademicYear",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYear",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lesson_Grade",
                        column: x => x.GradeId,
                        principalTable: "Grade",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lesson_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lesson_User",
                        column: x => x.LessonOwnerId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lesson_User1",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lesson_User2",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Question = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuestionRT = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    AcademicYearId = table.Column<int>(type: "int", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_AcademicYear",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYear",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Question_Grade",
                        column: x => x.GradeId,
                        principalTable: "Grade",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Question_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Question_User",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubjectTeachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeAllocatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectTeachers_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubjectTeachers_User",
                        column: x => x.TeacherId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClassSubject",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    SubjectTeacherId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSubject", x => new { x.ClassId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_ClassSubject_Class",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassSubject_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassSubject_User",
                        column: x => x.SubjectTeacherId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClassSubjectTimeTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSubjectTimeTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSubjectTimeTable_Class",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassSubjectTimeTable_Days",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassSubjectTimeTable_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudentClass",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    RemovedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClass", x => new { x.ClassId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentClass_Class",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentClass_User",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AssessmentClass",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    AssessmentId = table.Column<int>(type: "int", nullable: false),
                    PublishedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentClass", x => new { x.ClassId, x.AssessmentId });
                    table.ForeignKey(
                        name: "FK_AssessmentClass_Assessment",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssessmentClass_Class",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AssessmentClassStudent",
                columns: table => new
                {
                    AssessmentId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    ScorePrecentaged = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    StartedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CompletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ConnectedIP = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConnectedBrowser = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentClassStudent", x => new { x.AssessmentId, x.StudentId, x.ClassId });
                    table.ForeignKey(
                        name: "FK_AssessmentClassStudent_Assessment",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssessmentClassStudent_Class1",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssessmentClassStudent_User",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AssessmentSection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AssessmentId = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<int>(type: "int", nullable: false),
                    Instructions = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SectionContent = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentSection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentSection_Assessment",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudentAssessmentScore",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    AssessmentId = table.Column<int>(type: "int", nullable: false),
                    PredictedTargetScore = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    TargetGeneratedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    TeacherAdjustedTargetScore = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    TargetAdjustedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActualScore = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    ActualScoreEnteredOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAssessmentScore", x => new { x.StudentId, x.AssessmentId });
                    table.ForeignKey(
                        name: "FK_StudentAssessmentTarget_Assessment",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentAssessmentTarget_User",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonAssignedClass",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    StartedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonAssignedClass", x => new { x.ClassId, x.LessonId });
                    table.ForeignKey(
                        name: "FK_LessonAssignedClass_Class",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LessonAssignedClass_Lesson",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Instruction = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClosingDateTime = table.Column<string>(type: "char(10)", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonAssignment_Lesson",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonLearningOutcome",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    LearningOutcome = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonLearningOutcome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonLearningOutcome_Lesson",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonPrerequisites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    Prerequisite = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonPrerequisites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonPrerequisites_Lesson",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonTopic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LessonId = table.Column<int>(type: "int", nullable: true),
                    SequenceNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTopic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonTopic_Lesson",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonUnitTest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StudentGuide = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LessonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonUnitTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonUnitTest_Lesson",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionMCQTeacherAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AnswerTextRT = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SequenceNO = table.Column<int>(type: "int", nullable: false),
                    IsCorrectAnswer = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionMCQTeacherAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionMCQTeacherAnswer_Question",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionOpenEndedTeacherAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerText = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AnswerTextRT = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOpenEndedTeacherAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOpenEndedTeacherAnswer_Question",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionStructured",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuestionTextRT = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionStructured", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionStructured_Question",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubjectAttendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    TimeSlotId = table.Column<int>(type: "int", nullable: true),
                    IsExtraClass = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsReScheduleClass = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    LessonDetails = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsedSoftwareName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ActualEnteredDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectAttendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendance_Class",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attendance_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubjectAttendance_ClassSubjectTimeTable",
                        column: x => x.TimeSlotId,
                        principalTable: "ClassSubjectTimeTable",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AssessmentSectionQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AssessementSectionId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentSectionQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentSectionQuestion_AssessmentSection",
                        column: x => x.AssessementSectionId,
                        principalTable: "AssessmentSection",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssessmentSectionQuestion_Question",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonAssignmentStudent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonAssignmentId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    StudentRemarks = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TeacherComment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Score = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    SubmittedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    StudentIP = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StudentBrowser = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonAssignmentStudent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonAssignmentStudent_LessonAssignment",
                        column: x => x.LessonAssignmentId,
                        principalTable: "LessonAssignment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LessonAssignmentStudent_User",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonLecture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    LectureContentTypeId = table.Column<int>(type: "int", nullable: true),
                    LectureContent = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MIMEType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonLecture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonLecture_LessonLectureContentType",
                        column: x => x.LectureContentTypeId,
                        principalTable: "LessonLectureContentType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LessonLecture_LessonTopic",
                        column: x => x.TopicId,
                        principalTable: "LessonTopic",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonUnitTestTopic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonUnitTestId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Instruction = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonUnitTestTopic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonUnitTestTopic_LessonUnitTest",
                        column: x => x.LessonUnitTestId,
                        principalTable: "LessonUnitTest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LessonUnitTestTopic_QuestionType",
                        column: x => x.QuestionTypeId,
                        principalTable: "QuestionType",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionTructuredTeacherAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionStructuredId = table.Column<int>(type: "int", nullable: false),
                    AnswerText = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AnswerTextRT = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTructuredTeacherAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionTructuredTeacherAnswer_QuestionStructured",
                        column: x => x.QuestionStructuredId,
                        principalTable: "QuestionStructured",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudentSubjectAttendance",
                columns: table => new
                {
                    SubjectAttendanceId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    IsAttended = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubjectAttendance", x => new { x.SubjectAttendanceId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentSubjectAttendance_SubjectAttendance",
                        column: x => x.SubjectAttendanceId,
                        principalTable: "SubjectAttendance",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSubjectAttendance_User",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AssessmentSectionStudentQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AssessmentSectionQuestionId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Score = table.Column<decimal>(type: "decimal(6,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentSectionStudentQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentSectionStudentQuestion_AssessmentSectionQuestion",
                        column: x => x.AssessmentSectionQuestionId,
                        principalTable: "AssessmentSectionQuestion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssessmentSectionStudentQuestion_User",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonAssignmentStudentUpload",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonAssignmentStudentId = table.Column<int>(type: "int", nullable: false),
                    UploadFilePath = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UploadedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonAssignmentStudentUpload", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonAssignmentStudentUpload_LessonAssignmentStudent",
                        column: x => x.LessonAssignmentStudentId,
                        principalTable: "LessonAssignmentStudent",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonUnitTestTopicQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonUnitTestTopicId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(6,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonUnitTestTopicQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonUnitTestTopicQuestion_LessonUnitTestTopic",
                        column: x => x.LessonUnitTestTopicId,
                        principalTable: "LessonUnitTestTopic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LessonUnitTestTopicQuestion_Question",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AssessmentMCQQuestionStudentAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AssessmentSectionStudentQuestionId = table.Column<int>(type: "int", nullable: false),
                    TeacherAnswerId = table.Column<int>(type: "int", nullable: false),
                    IsSelected = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsSelectionCorrect = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SubmittedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentMCQQuestionStudentAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentMCQQuestionStudentAnswer_AssessmentSectionStudentQuestion",
                        column: x => x.AssessmentSectionStudentQuestionId,
                        principalTable: "AssessmentSectionStudentQuestion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssessmentMCQQuestionStudentAnswer_QuestionMCQTeacherAnswer",
                        column: x => x.TeacherAnswerId,
                        principalTable: "QuestionMCQTeacherAnswer",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AssessmentOpenEndedQuestionStudentAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AssessmentSectionStudentQuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AnswerTextRT = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TeacherComment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubmittedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentOpenEndedQuestionStudentAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentOpenEndedQuestionStudentAnswer_AssessmentSectionStudentQuestion",
                        column: x => x.AssessmentSectionStudentQuestionId,
                        principalTable: "AssessmentSectionStudentQuestion",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AssessmentStructuredQuestionStudentAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AssessmentSectionStudentQuestionId = table.Column<int>(type: "int", nullable: false),
                    StructuredQuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AnswerTextRT = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubmittedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentStructuredQuestionStudentAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentStructuredQuestionStudentAnswer_AssessmentSectionStudentQuestion",
                        column: x => x.AssessmentSectionStudentQuestionId,
                        principalTable: "AssessmentSectionStudentQuestion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssessmentStructuredQuestionStudentAnswer_QuestionStructured",
                        column: x => x.StructuredQuestionId,
                        principalTable: "QuestionStructured",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonUnitTestTopicStudentQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonUnitTestTopicQuestionId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Score = table.Column<decimal>(type: "decimal(6,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonUnitTestTopicStudentQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonUnitTestTopicStudentQuestion_LessonUnitTestTopicQuestion",
                        column: x => x.LessonUnitTestTopicQuestionId,
                        principalTable: "LessonUnitTestTopicQuestion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LessonUnitTestTopicStudentQuestion_User",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonUnitTestTopicStudentMCQQuestionAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonUnitTestTopicStudentQuestionId = table.Column<int>(type: "int", nullable: false),
                    TeacherAnswerId = table.Column<int>(type: "int", nullable: false),
                    IsSelected = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsSelectionCorrect = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonUnitTestTopicStudentMCQQuestionAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonUnitTestTopicStudentMCQQuestionAnswer_LessonUnitTestTopicStudentQuestion",
                        column: x => x.LessonUnitTestTopicStudentQuestionId,
                        principalTable: "LessonUnitTestTopicStudentQuestion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LessonUnitTestTopicStudentMCQQuestionAnswer_QuestionMCQTeacherAnswer",
                        column: x => x.TeacherAnswerId,
                        principalTable: "QuestionMCQTeacherAnswer",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonUnitTestTopicStudentOpenEndedQuestionAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonUnitTestTopicStudentQuestionId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AnswerRT = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TeacherComment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonUnitTestTopicStudentOpenEndedQuestionAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonUnitTestTopicStudentOpenEndedQuestionAnswer_LessonUnitTestTopicStudentQuestion",
                        column: x => x.LessonUnitTestTopicStudentQuestionId,
                        principalTable: "LessonUnitTestTopicStudentQuestion",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_AcademicYearId",
                table: "Assessment",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_ApprovedBy",
                table: "Assessment",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_AssessmentTypeId",
                table: "Assessment",
                column: "AssessmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_CreatedById",
                table: "Assessment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_GradeId",
                table: "Assessment",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_SubjectId",
                table: "Assessment",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_UpdatedById",
                table: "Assessment",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentClass_AssessmentId",
                table: "AssessmentClass",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentClassStudent_ClassId",
                table: "AssessmentClassStudent",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentClassStudent_StudentId",
                table: "AssessmentClassStudent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentMCQQuestionStudentAnswer_AssessmentSectionStudentQ~",
                table: "AssessmentMCQQuestionStudentAnswer",
                column: "AssessmentSectionStudentQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentMCQQuestionStudentAnswer_TeacherAnswerId",
                table: "AssessmentMCQQuestionStudentAnswer",
                column: "TeacherAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentOpenEndedQuestionStudentAnswer_AssessmentSectionSt~",
                table: "AssessmentOpenEndedQuestionStudentAnswer",
                column: "AssessmentSectionStudentQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSection_AssessmentId",
                table: "AssessmentSection",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSectionQuestion_AssessementSectionId",
                table: "AssessmentSectionQuestion",
                column: "AssessementSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSectionQuestion_QuestionId",
                table: "AssessmentSectionQuestion",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSectionStudentQuestion_AssessmentSectionQuestionId",
                table: "AssessmentSectionStudentQuestion",
                column: "AssessmentSectionQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSectionStudentQuestion_StudentId",
                table: "AssessmentSectionStudentQuestion",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentStructuredQuestionStudentAnswer_AssessmentSectionS~",
                table: "AssessmentStructuredQuestionStudentAnswer",
                column: "AssessmentSectionStudentQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentStructuredQuestionStudentAnswer_StructuredQuestion~",
                table: "AssessmentStructuredQuestionStudentAnswer",
                column: "StructuredQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_AcademicYear",
                table: "Class",
                column: "AcademicYear");

            migrationBuilder.CreateIndex(
                name: "IX_Class_ClassTeacherId",
                table: "Class",
                column: "ClassTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_GradeId",
                table: "Class",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubject_SubjectId",
                table: "ClassSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubject_SubjectTeacherId",
                table: "ClassSubject",
                column: "SubjectTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjectTimeTable_ClassId",
                table: "ClassSubjectTimeTable",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjectTimeTable_DayId",
                table: "ClassSubjectTimeTable",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjectTimeTable_SubjectId",
                table: "ClassSubjectTimeTable",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_LevelHeadId",
                table: "Grade",
                column: "LevelHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeSubject_GradeId",
                table: "GradeSubject",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_AcademicYearId",
                table: "Lesson",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_CreatedById",
                table: "Lesson",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_GradeId",
                table: "Lesson",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_LessonOwnerId",
                table: "Lesson",
                column: "LessonOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_SubjectId",
                table: "Lesson",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_UpdatedById",
                table: "Lesson",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_LessonAssignedClass_LessonId",
                table: "LessonAssignedClass",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonAssignment_LessonId",
                table: "LessonAssignment",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonAssignmentStudent_LessonAssignmentId",
                table: "LessonAssignmentStudent",
                column: "LessonAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonAssignmentStudent_StudentId",
                table: "LessonAssignmentStudent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonAssignmentStudentUpload_LessonAssignmentStudentId",
                table: "LessonAssignmentStudentUpload",
                column: "LessonAssignmentStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonLearningOutcome_LessonId",
                table: "LessonLearningOutcome",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonLecture_LectureContentTypeId",
                table: "LessonLecture",
                column: "LectureContentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonLecture_TopicId",
                table: "LessonLecture",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonPrerequisites_LessonId",
                table: "LessonPrerequisites",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTopic_LessonId",
                table: "LessonTopic",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUnitTest_LessonId",
                table: "LessonUnitTest",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUnitTestTopic_LessonUnitTestId",
                table: "LessonUnitTestTopic",
                column: "LessonUnitTestId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUnitTestTopic_QuestionTypeId",
                table: "LessonUnitTestTopic",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUnitTestTopicQuestion_LessonUnitTestTopicId",
                table: "LessonUnitTestTopicQuestion",
                column: "LessonUnitTestTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUnitTestTopicQuestion_QuestionId",
                table: "LessonUnitTestTopicQuestion",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUnitTestTopicStudentMCQQuestionAnswer_LessonUnitTestTo~",
                table: "LessonUnitTestTopicStudentMCQQuestionAnswer",
                column: "LessonUnitTestTopicStudentQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUnitTestTopicStudentMCQQuestionAnswer_TeacherAnswerId",
                table: "LessonUnitTestTopicStudentMCQQuestionAnswer",
                column: "TeacherAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUnitTestTopicStudentOpenEndedQuestionAnswer_LessonUnit~",
                table: "LessonUnitTestTopicStudentOpenEndedQuestionAnswer",
                column: "LessonUnitTestTopicStudentQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUnitTestTopicStudentQuestion_LessonUnitTestTopicQuesti~",
                table: "LessonUnitTestTopicStudentQuestion",
                column: "LessonUnitTestTopicQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUnitTestTopicStudentQuestion_StudentId",
                table: "LessonUnitTestTopicStudentQuestion",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_AcademicYearId",
                table: "Question",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_GradeId",
                table: "Question",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_OwnerId",
                table: "Question",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_SubjectId",
                table: "Question",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionMCQTeacherAnswer_QuestionId",
                table: "QuestionMCQTeacherAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOpenEndedTeacherAnswer_QuestionId",
                table: "QuestionOpenEndedTeacherAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionStructured_QuestionId",
                table: "QuestionStructured",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTructuredTeacherAnswer_QuestionStructuredId",
                table: "QuestionTructuredTeacherAnswer",
                column: "QuestionStructuredId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessmentScore_AssessmentId",
                table: "StudentAssessmentScore",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClass_StudentId",
                table: "StudentClass",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjectAttendance_StudentId",
                table: "StudentSubjectAttendance",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_DepartmentHeadId",
                table: "Subject",
                column: "DepartmentHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_ParentSubjectId",
                table: "Subject",
                column: "ParentSubjectId");

            migrationBuilder.CreateIndex(
                name: "UQ__Subject__737584F6FE2978E8",
                table: "Subject",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAttendance_ClassId",
                table: "SubjectAttendance",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAttendance_SubjectId",
                table: "SubjectAttendance",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAttendance_TimeSlotId",
                table: "SubjectAttendance",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeachers_SubjectId",
                table: "SubjectTeachers",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeachers_TeacherId",
                table: "SubjectTeachers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "UQ__User__536C85E4FCCFF6D0",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentClass");

            migrationBuilder.DropTable(
                name: "AssessmentClassStudent");

            migrationBuilder.DropTable(
                name: "AssessmentMCQQuestionStudentAnswer");

            migrationBuilder.DropTable(
                name: "AssessmentOpenEndedQuestionStudentAnswer");

            migrationBuilder.DropTable(
                name: "AssessmentStructuredQuestionStudentAnswer");

            migrationBuilder.DropTable(
                name: "ClassSubject");

            migrationBuilder.DropTable(
                name: "ClassSubjectStudent");

            migrationBuilder.DropTable(
                name: "GradeSubject");

            migrationBuilder.DropTable(
                name: "LessonAssignedClass");

            migrationBuilder.DropTable(
                name: "LessonAssignmentStudentUpload");

            migrationBuilder.DropTable(
                name: "LessonLearningOutcome");

            migrationBuilder.DropTable(
                name: "LessonLecture");

            migrationBuilder.DropTable(
                name: "LessonPrerequisites");

            migrationBuilder.DropTable(
                name: "LessonUnitTestTopicStudentMCQQuestionAnswer");

            migrationBuilder.DropTable(
                name: "LessonUnitTestTopicStudentOpenEndedQuestionAnswer");

            migrationBuilder.DropTable(
                name: "QuestionOpenEndedTeacherAnswer");

            migrationBuilder.DropTable(
                name: "QuestionTructuredTeacherAnswer");

            migrationBuilder.DropTable(
                name: "StudentAssessmentScore");

            migrationBuilder.DropTable(
                name: "StudentClass");

            migrationBuilder.DropTable(
                name: "StudentSubjectAttendance");

            migrationBuilder.DropTable(
                name: "SubjectTeachers");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "AssessmentSectionStudentQuestion");

            migrationBuilder.DropTable(
                name: "LessonAssignmentStudent");

            migrationBuilder.DropTable(
                name: "LessonLectureContentType");

            migrationBuilder.DropTable(
                name: "LessonTopic");

            migrationBuilder.DropTable(
                name: "QuestionMCQTeacherAnswer");

            migrationBuilder.DropTable(
                name: "LessonUnitTestTopicStudentQuestion");

            migrationBuilder.DropTable(
                name: "QuestionStructured");

            migrationBuilder.DropTable(
                name: "SubjectAttendance");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "AssessmentSectionQuestion");

            migrationBuilder.DropTable(
                name: "LessonAssignment");

            migrationBuilder.DropTable(
                name: "LessonUnitTestTopicQuestion");

            migrationBuilder.DropTable(
                name: "ClassSubjectTimeTable");

            migrationBuilder.DropTable(
                name: "AssessmentSection");

            migrationBuilder.DropTable(
                name: "LessonUnitTestTopic");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Assessment");

            migrationBuilder.DropTable(
                name: "LessonUnitTest");

            migrationBuilder.DropTable(
                name: "QuestionType");

            migrationBuilder.DropTable(
                name: "AssessmentType");

            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "AcademicYear");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
