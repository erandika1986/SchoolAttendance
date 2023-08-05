using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JobApp00005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsParentSubject",
                table: "Subject",
                type: "tinyint(1)",
                nullable: false,
                defaultValueSql: "((0))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsParentSubject",
                table: "Subject");
        }
    }
}
