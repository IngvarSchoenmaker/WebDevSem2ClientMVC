using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevSem2ClientMVC.Migrations
{
    /// <inheritdoc />
    public partial class connecttables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LobbyTable_UnoGame_GameId",
                table: "LobbyTable");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_UnoGame_GameId",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_UnoGameDeck_UnoGame_UnoGameId",
                table: "UnoGameDeck");

            migrationBuilder.DropForeignKey(
                name: "FK_UnoGameDiscardPile_UnoGame_UnoGame1Id",
                table: "UnoGameDiscardPile");

            migrationBuilder.RenameColumn(
                name: "UnoGame1Id",
                table: "UnoGameDiscardPile",
                newName: "UnoGame1UnoId");

            migrationBuilder.RenameIndex(
                name: "IX_UnoGameDiscardPile_UnoGame1Id",
                table: "UnoGameDiscardPile",
                newName: "IX_UnoGameDiscardPile_UnoGame1UnoId");

            migrationBuilder.RenameColumn(
                name: "UnoGameId",
                table: "UnoGameDeck",
                newName: "UnoGameUnoId");

            migrationBuilder.RenameIndex(
                name: "IX_UnoGameDeck_UnoGameId",
                table: "UnoGameDeck",
                newName: "IX_UnoGameDeck_UnoGameUnoId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UnoGame",
                newName: "UnoId");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Player",
                newName: "GameUnoId");

            migrationBuilder.RenameIndex(
                name: "IX_Player_GameId",
                table: "Player",
                newName: "IX_Player_GameUnoId");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "LobbyTable",
                newName: "UnoId");

            migrationBuilder.RenameIndex(
                name: "IX_LobbyTable_GameId",
                table: "LobbyTable",
                newName: "IX_LobbyTable_UnoId");

            migrationBuilder.AddForeignKey(
                name: "FK_LobbyTable_UnoGame_UnoId",
                table: "LobbyTable",
                column: "UnoId",
                principalTable: "UnoGame",
                principalColumn: "UnoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_UnoGame_GameUnoId",
                table: "Player",
                column: "GameUnoId",
                principalTable: "UnoGame",
                principalColumn: "UnoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnoGameDeck_UnoGame_UnoGameUnoId",
                table: "UnoGameDeck",
                column: "UnoGameUnoId",
                principalTable: "UnoGame",
                principalColumn: "UnoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnoGameDiscardPile_UnoGame_UnoGame1UnoId",
                table: "UnoGameDiscardPile",
                column: "UnoGame1UnoId",
                principalTable: "UnoGame",
                principalColumn: "UnoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LobbyTable_UnoGame_UnoId",
                table: "LobbyTable");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_UnoGame_GameUnoId",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_UnoGameDeck_UnoGame_UnoGameUnoId",
                table: "UnoGameDeck");

            migrationBuilder.DropForeignKey(
                name: "FK_UnoGameDiscardPile_UnoGame_UnoGame1UnoId",
                table: "UnoGameDiscardPile");

            migrationBuilder.RenameColumn(
                name: "UnoGame1UnoId",
                table: "UnoGameDiscardPile",
                newName: "UnoGame1Id");

            migrationBuilder.RenameIndex(
                name: "IX_UnoGameDiscardPile_UnoGame1UnoId",
                table: "UnoGameDiscardPile",
                newName: "IX_UnoGameDiscardPile_UnoGame1Id");

            migrationBuilder.RenameColumn(
                name: "UnoGameUnoId",
                table: "UnoGameDeck",
                newName: "UnoGameId");

            migrationBuilder.RenameIndex(
                name: "IX_UnoGameDeck_UnoGameUnoId",
                table: "UnoGameDeck",
                newName: "IX_UnoGameDeck_UnoGameId");

            migrationBuilder.RenameColumn(
                name: "UnoId",
                table: "UnoGame",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GameUnoId",
                table: "Player",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Player_GameUnoId",
                table: "Player",
                newName: "IX_Player_GameId");

            migrationBuilder.RenameColumn(
                name: "UnoId",
                table: "LobbyTable",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_LobbyTable_UnoId",
                table: "LobbyTable",
                newName: "IX_LobbyTable_GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_LobbyTable_UnoGame_GameId",
                table: "LobbyTable",
                column: "GameId",
                principalTable: "UnoGame",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_UnoGame_GameId",
                table: "Player",
                column: "GameId",
                principalTable: "UnoGame",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnoGameDeck_UnoGame_UnoGameId",
                table: "UnoGameDeck",
                column: "UnoGameId",
                principalTable: "UnoGame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnoGameDiscardPile_UnoGame_UnoGame1Id",
                table: "UnoGameDiscardPile",
                column: "UnoGame1Id",
                principalTable: "UnoGame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
