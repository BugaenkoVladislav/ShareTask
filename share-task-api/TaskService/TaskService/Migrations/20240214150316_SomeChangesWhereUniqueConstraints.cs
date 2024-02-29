using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthorizeService.Migrations
{
    /// <inheritdoc />
    public partial class SomeChangesWhereUniqueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Midname",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_LoginPasswords_Login",
                table: "LoginPasswords",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoginPasswords_Login",
                table: "LoginPasswords");

            migrationBuilder.AddColumn<string>(
                name: "Midname",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
