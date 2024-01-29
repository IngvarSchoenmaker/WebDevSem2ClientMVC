using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevSem2ClientMVC.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_UnoGame_GameUnoId",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "GameUnoId",
                table: "Player",
                newName: "UnoGameUnoId");

            migrationBuilder.RenameIndex(
                name: "IX_Player_GameUnoId",
                table: "Player",
                newName: "IX_Player_UnoGameUnoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_UnoGame_UnoGameUnoId",
                table: "Player",
                column: "UnoGameUnoId",
                principalTable: "UnoGame",
                principalColumn: "UnoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_UnoGame_UnoGameUnoId",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "UnoGameUnoId",
                table: "Player",
                newName: "GameUnoId");

            migrationBuilder.RenameIndex(
                name: "IX_Player_UnoGameUnoId",
                table: "Player",
                newName: "IX_Player_GameUnoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_UnoGame_GameUnoId",
                table: "Player",
                column: "GameUnoId",
                principalTable: "UnoGame",
                principalColumn: "UnoId");
        }
    }
}
