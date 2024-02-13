using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShareTaskAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddListsAndMadeForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersLists",
                columns: table => new
                {
                    idUserList = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idUser = table.Column<long>(type: "bigint", nullable: false),
                    idList = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UsersLists_pkey", x => x.idUserList);
                    table.ForeignKey(
                        name: "UsersLists_idList_fkey",
                        column: x => x.idList,
                        principalTable: "Lists",
                        principalColumn: "idList");
                    table.ForeignKey(
                        name: "UsersLists_idUser_fkey",
                        column: x => x.idUser,
                        principalTable: "Users",
                        principalColumn: "idUser");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersLists_idList",
                table: "UsersLists",
                column: "idList");

            migrationBuilder.CreateIndex(
                name: "IX_UsersLists_idUser",
                table: "UsersLists",
                column: "idUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersLists");
        }
    }
}
