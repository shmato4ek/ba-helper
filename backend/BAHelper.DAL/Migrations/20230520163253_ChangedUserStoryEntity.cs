using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BAHelper.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUserStoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcceptanceCriterias_UserStories_UserStoryId1",
                table: "AcceptanceCriterias");

            migrationBuilder.DropForeignKey(
                name: "FK_UserStoryFormulas_UserStories_UserStoryId1",
                table: "UserStoryFormulas");

            migrationBuilder.DropIndex(
                name: "IX_UserStoryFormulas_UserStoryId1",
                table: "UserStoryFormulas");

            migrationBuilder.DropIndex(
                name: "IX_AcceptanceCriterias_UserStoryId1",
                table: "AcceptanceCriterias");

            migrationBuilder.DropColumn(
                name: "UserStoryId1",
                table: "UserStoryFormulas");

            migrationBuilder.DropColumn(
                name: "UserStoryId1",
                table: "AcceptanceCriterias");

            migrationBuilder.RenameColumn(
                name: "ArchivedData",
                table: "Projects",
                newName: "ArchivedDate");

            migrationBuilder.AddColumn<List<string>>(
                name: "AcceptanceCriterias",
                table: "UserStories",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "Formulas",
                table: "UserStories",
                type: "text[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptanceCriterias",
                table: "UserStories");

            migrationBuilder.DropColumn(
                name: "Formulas",
                table: "UserStories");

            migrationBuilder.RenameColumn(
                name: "ArchivedDate",
                table: "Projects",
                newName: "ArchivedData");

            migrationBuilder.AddColumn<int>(
                name: "UserStoryId1",
                table: "UserStoryFormulas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserStoryId1",
                table: "AcceptanceCriterias",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserStoryFormulas_UserStoryId1",
                table: "UserStoryFormulas",
                column: "UserStoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_AcceptanceCriterias_UserStoryId1",
                table: "AcceptanceCriterias",
                column: "UserStoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AcceptanceCriterias_UserStories_UserStoryId1",
                table: "AcceptanceCriterias",
                column: "UserStoryId1",
                principalTable: "UserStories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStoryFormulas_UserStories_UserStoryId1",
                table: "UserStoryFormulas",
                column: "UserStoryId1",
                principalTable: "UserStories",
                principalColumn: "Id");
        }
    }
}
