using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JobApp00002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubject_User",
                table: "ClassSubject");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectTeacherId",
                table: "ClassSubject",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubject_User",
                table: "ClassSubject",
                column: "SubjectTeacherId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubject_User",
                table: "ClassSubject");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectTeacherId",
                table: "ClassSubject",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubject_User",
                table: "ClassSubject",
                column: "SubjectTeacherId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
