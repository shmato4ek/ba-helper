using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BAHelper.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DeletedFieldsInSubtask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Subtasks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Subtasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "Subtasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Subtasks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
