using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BAHelper.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsDeletedFieldsForDocAndProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Projects",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Documents",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Documents");
        }
    }
}
