using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevSem2ClientMVC.Migrations
{
    /// <inheritdoc />
    public partial class connectedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnoGameDeck_Card_DeckId",
                table: "UnoGameDeck");

            migrationBuilder.DropForeignKey(
                name: "FK_UnoGameDiscardPile_Card_DiscardPileId",
                table: "UnoGameDiscardPile");

            migrationBuilder.RenameColumn(
                name: "DiscardPileId",
                table: "UnoGameDiscardPile",
                newName: "DiscardPileCardId");

            migrationBuilder.RenameColumn(
                name: "DeckId",
                table: "UnoGameDeck",
                newName: "DeckCardId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Player",
                newName: "PlayerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Card",
                newName: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnoGameDeck_Card_DeckCardId",
                table: "UnoGameDeck",
                column: "DeckCardId",
                principalTable: "Card",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnoGameDiscardPile_Card_DiscardPileCardId",
                table: "UnoGameDiscardPile",
                column: "DiscardPileCardId",
                principalTable: "Card",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnoGameDeck_Card_DeckCardId",
                table: "UnoGameDeck");

            migrationBuilder.DropForeignKey(
                name: "FK_UnoGameDiscardPile_Card_DiscardPileCardId",
                table: "UnoGameDiscardPile");

            migrationBuilder.RenameColumn(
                name: "DiscardPileCardId",
                table: "UnoGameDiscardPile",
                newName: "DiscardPileId");

            migrationBuilder.RenameColumn(
                name: "DeckCardId",
                table: "UnoGameDeck",
                newName: "DeckId");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Player",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "Card",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnoGameDeck_Card_DeckId",
                table: "UnoGameDeck",
                column: "DeckId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnoGameDiscardPile_Card_DiscardPileId",
                table: "UnoGameDiscardPile",
                column: "DiscardPileId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
