using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamService.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAdminSElection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_IdAdmin",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_IdAdmin",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "IdAdmin",
                table: "Teams");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "TeamsUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "TeamsUsers");

            migrationBuilder.AddColumn<long>(
                name: "IdAdmin",
                table: "Teams",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_IdAdmin",
                table: "Teams",
                column: "IdAdmin");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_IdAdmin",
                table: "Teams",
                column: "IdAdmin",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
