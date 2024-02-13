using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareTaskAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedAnotherOneColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "idCreator",
                table: "Lists",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Lists_idCreator",
                table: "Lists",
                column: "idCreator");

            migrationBuilder.AddForeignKey(
                name: "Lists_idCreator_fkey",
                table: "Lists",
                column: "idCreator",
                principalTable: "Users",
                principalColumn: "idUser",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Lists_idCreator_fkey",
                table: "Lists");

            migrationBuilder.DropIndex(
                name: "IX_Lists_idCreator",
                table: "Lists");

            migrationBuilder.DropColumn(
                name: "idCreator",
                table: "Lists");
        }
    }
}
