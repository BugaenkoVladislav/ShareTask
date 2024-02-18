using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthorizeService.Migrations
{
    /// <inheritdoc />
    public partial class AddedForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_IdLoginPassword",
                table: "Users",
                column: "IdLoginPassword");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdProfession",
                table: "Users",
                column: "IdProfession");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LoginPasswords_IdLoginPassword",
                table: "Users",
                column: "IdLoginPassword",
                principalTable: "LoginPasswords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Professions_IdProfession",
                table: "Users",
                column: "IdProfession",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_LoginPasswords_IdLoginPassword",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Professions_IdProfession",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IdLoginPassword",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IdProfession",
                table: "Users");
        }
    }
}
