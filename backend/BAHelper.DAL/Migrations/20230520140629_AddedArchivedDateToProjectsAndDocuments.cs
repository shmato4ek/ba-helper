using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BAHelper.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedArchivedDateToProjectsAndDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clusters_Projects_ProjectId",
                table: "Clusters");

            migrationBuilder.DropIndex(
                name: "IX_Clusters_ProjectId",
                table: "Clusters");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArchivedData",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ArchivedDate",
                table: "Documents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchivedData",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ArchivedDate",
                table: "Documents");

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_ProjectId",
                table: "Clusters",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clusters_Projects_ProjectId",
                table: "Clusters",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
