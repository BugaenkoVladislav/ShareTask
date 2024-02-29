using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthorizeService.Migrations
{
    /// <inheritdoc />
    public partial class UniqueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_IdLoginPassword",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdLoginPassword",
                table: "Users",
                column: "IdLoginPassword",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Phone",
                table: "Users",
                column: "Phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_IdLoginPassword",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Phone",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdLoginPassword",
                table: "Users",
                column: "IdLoginPassword");
        }
    }
}
