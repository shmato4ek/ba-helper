using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BAHelper.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedConfigurationForProjectTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtasks_Tasks_ProjectTaskId",
                table: "Subtasks");

            migrationBuilder.DropIndex(
                name: "IX_Subtasks_ProjectTaskId",
                table: "Subtasks");

            migrationBuilder.DropColumn(
                name: "ProjectTaskId",
                table: "Subtasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectTaskId",
                table: "Subtasks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subtasks_ProjectTaskId",
                table: "Subtasks",
                column: "ProjectTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtasks_Tasks_ProjectTaskId",
                table: "Subtasks",
                column: "ProjectTaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
