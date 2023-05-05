using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BAHelper.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedHoursForTasksAndProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Hours",
                table: "Tasks",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Hours",
                table: "Projects",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hours",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "Projects");
        }
    }
}
