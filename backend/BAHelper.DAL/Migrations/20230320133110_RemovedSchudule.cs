using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BAHelper.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSchudule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoursOfWork",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Schedule",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TimeForProject",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Projects",
                newName: "AuthorId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadine",
                table: "Tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deadine",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Projects",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "HoursOfWork",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int[]>(
                name: "Schedule",
                table: "Users",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);

            migrationBuilder.AddColumn<double>(
                name: "TimeForProject",
                table: "Tasks",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
