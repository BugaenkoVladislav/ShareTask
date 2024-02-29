using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskService.Migrations
{
    /// <inheritdoc />
    public partial class Statuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdStatus",
                table: "Tasks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdStatus",
                table: "TaskExecutors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_IdStatus",
                table: "Tasks",
                column: "IdStatus");

            migrationBuilder.CreateIndex(
                name: "IX_TaskExecutors_IdStatus",
                table: "TaskExecutors",
                column: "IdStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskExecutors_Statuses_IdStatus",
                table: "TaskExecutors",
                column: "IdStatus",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Statuses_IdStatus",
                table: "Tasks",
                column: "IdStatus",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskExecutors_Statuses_IdStatus",
                table: "TaskExecutors");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Statuses_IdStatus",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_IdStatus",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_TaskExecutors_IdStatus",
                table: "TaskExecutors");

            migrationBuilder.DropColumn(
                name: "IdStatus",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IdStatus",
                table: "TaskExecutors");
        }
    }
}
