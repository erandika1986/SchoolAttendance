using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JobApp00004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Statue",
                table: "Assessment");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "User",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "User",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "User",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "User",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "VersionNo",
                table: "Assessment",
                newName: "Status");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Lesson",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<int>(
                name: "UpdatedById",
                table: "Lesson",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "Lesson",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "AssessmentUpload",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<int>(
                name: "UpdatedById",
                table: "AssessmentUpload",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "AssessmentUpload",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Assessment",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "User",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "User",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "User",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "User",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Assessment",
                newName: "VersionNo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Lesson",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UpdatedById",
                table: "Lesson",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "Lesson",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "AssessmentUpload",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UpdatedById",
                table: "AssessmentUpload",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "AssessmentUpload",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Assessment",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Statue",
                table: "Assessment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
