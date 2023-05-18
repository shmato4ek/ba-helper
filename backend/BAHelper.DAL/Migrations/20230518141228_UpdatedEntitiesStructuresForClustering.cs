using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BAHelper.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntitiesStructuresForClustering : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clusters_ClusteredStatistics_ClusteredStatisticId",
                table: "Clusters");

            migrationBuilder.DropTable(
                name: "ClusteredStatistics");

            migrationBuilder.DropIndex(
                name: "IX_Clusters_ClusteredStatisticId",
                table: "Clusters");

            migrationBuilder.DropColumn(
                name: "ClusteredStatisticId",
                table: "Clusters");

            migrationBuilder.DropColumn(
                name: "Quality",
                table: "Clusters");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Clusters");

            migrationBuilder.DropColumn(
                name: "StatisticId",
                table: "Clusters");

            migrationBuilder.RenameColumn(
                name: "Topic",
                table: "Clusters",
                newName: "ProjectId");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnoughData",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "TaskEnd",
                table: "Tasks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TaskStart",
                table: "Tasks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TaskTopic",
                table: "Statistics",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<int>(
                name: "TaskCount",
                table: "Statistics",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ClustersData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClusterId = table.Column<int>(type: "integer", nullable: false),
                    Topic = table.Column<int>(type: "integer", nullable: false),
                    Quality = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClustersData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClustersData_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClusterUser",
                columns: table => new
                {
                    ClustersId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterUser", x => new { x.ClustersId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ClusterUser_Clusters_ClustersId",
                        column: x => x.ClustersId,
                        principalTable: "Clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_ProjectId",
                table: "Clusters",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClustersData_ClusterId",
                table: "ClustersData",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterUser_UsersId",
                table: "ClusterUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clusters_Projects_ProjectId",
                table: "Clusters",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clusters_Projects_ProjectId",
                table: "Clusters");

            migrationBuilder.DropTable(
                name: "ClustersData");

            migrationBuilder.DropTable(
                name: "ClusterUser");

            migrationBuilder.DropIndex(
                name: "IX_Clusters_ProjectId",
                table: "Clusters");

            migrationBuilder.DropColumn(
                name: "IsEnoughData",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TaskEnd",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskStart",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskCount",
                table: "Statistics");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Clusters",
                newName: "Topic");

            migrationBuilder.AlterColumn<double>(
                name: "TaskTopic",
                table: "Statistics",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ClusteredStatisticId",
                table: "Clusters",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Quality",
                table: "Clusters",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Clusters",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "StatisticId",
                table: "Clusters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ClusteredStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusteredStatistics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_ClusteredStatisticId",
                table: "Clusters",
                column: "ClusteredStatisticId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clusters_ClusteredStatistics_ClusteredStatisticId",
                table: "Clusters",
                column: "ClusteredStatisticId",
                principalTable: "ClusteredStatistics",
                principalColumn: "Id");
        }
    }
}
