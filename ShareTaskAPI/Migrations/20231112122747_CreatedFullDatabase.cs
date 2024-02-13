using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShareTaskAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreatedFullDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "UsersLists_idList_fkey",
                table: "UsersLists");

            migrationBuilder.DropForeignKey(
                name: "UsersLists_idUser_fkey",
                table: "UsersLists");

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    IdTask = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idList = table.Column<long>(type: "bigint", nullable: false),
                    idCreator = table.Column<long>(type: "bigint", nullable: false),
                    IdRole = table.Column<long>(type: "bigint", nullable: false),
                    nameTask = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Tasks_pkey", x => x.IdTask);
                    table.ForeignKey(
                        name: "Tasks_idCreator_fkey",
                        column: x => x.idCreator,
                        principalTable: "Users",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Tasks_idList_fkey",
                        column: x => x.idList,
                        principalTable: "Lists",
                        principalColumn: "idList",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Tasks_idRole_fkey",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "idRole",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_idCreator",
                table: "Tasks",
                column: "idCreator");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_idList",
                table: "Tasks",
                column: "idList");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_IdRole",
                table: "Tasks",
                column: "IdRole");

            migrationBuilder.AddForeignKey(
                name: "UsersLists_idList_fkey",
                table: "UsersLists",
                column: "idList",
                principalTable: "Lists",
                principalColumn: "idList",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "UsersLists_idUser_fkey",
                table: "UsersLists",
                column: "idUser",
                principalTable: "Users",
                principalColumn: "idUser",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "UsersLists_idList_fkey",
                table: "UsersLists");

            migrationBuilder.DropForeignKey(
                name: "UsersLists_idUser_fkey",
                table: "UsersLists");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "UsersLists_idList_fkey",
                table: "UsersLists",
                column: "idList",
                principalTable: "Lists",
                principalColumn: "idList");

            migrationBuilder.AddForeignKey(
                name: "UsersLists_idUser_fkey",
                table: "UsersLists",
                column: "idUser",
                principalTable: "Users",
                principalColumn: "idUser");
        }
    }
}
