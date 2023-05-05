using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BAHelper.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedStatesToSubtasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskState",
                table: "Subtasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskState",
                table: "Subtasks");
        }
    }
}
