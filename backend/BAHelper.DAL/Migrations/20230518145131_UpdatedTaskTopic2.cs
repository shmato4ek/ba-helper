using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BAHelper.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTaskTopic2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskTopics_Tasks_ProjectTaskId",
                table: "TaskTopics");

            migrationBuilder.DropIndex(
                name: "IX_TaskTopics_ProjectTaskId",
                table: "TaskTopics");

            migrationBuilder.DropColumn(
                name: "ProjectTaskId",
                table: "TaskTopics");

            migrationBuilder.AddColumn<int[]>(
                name: "Tags",
                table: "Tasks",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "ProjectTaskId",
                table: "TaskTopics",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskTopics_ProjectTaskId",
                table: "TaskTopics",
                column: "ProjectTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTopics_Tasks_ProjectTaskId",
                table: "TaskTopics",
                column: "ProjectTaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
