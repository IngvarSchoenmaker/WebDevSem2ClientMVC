using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevSem2ClientMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddedCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_UnoGame_UnoGameId",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_UnoGame_UnoGameId1",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_UnoGameId",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_UnoGameId1",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "UnoGameId",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "UnoGameId1",
                table: "Card");

            migrationBuilder.CreateTable(
                name: "UnoGameDeck",
                columns: table => new
                {
                    DeckId = table.Column<int>(type: "int", nullable: false),
                    UnoGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnoGameDeck", x => new { x.DeckId, x.UnoGameId });
                    table.ForeignKey(
                        name: "FK_UnoGameDeck_Card_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnoGameDeck_UnoGame_UnoGameId",
                        column: x => x.UnoGameId,
                        principalTable: "UnoGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnoGameDiscardPile",
                columns: table => new
                {
                    DiscardPileId = table.Column<int>(type: "int", nullable: false),
                    UnoGame1Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnoGameDiscardPile", x => new { x.DiscardPileId, x.UnoGame1Id });
                    table.ForeignKey(
                        name: "FK_UnoGameDiscardPile_Card_DiscardPileId",
                        column: x => x.DiscardPileId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnoGameDiscardPile_UnoGame_UnoGame1Id",
                        column: x => x.UnoGame1Id,
                        principalTable: "UnoGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnoGameDeck_UnoGameId",
                table: "UnoGameDeck",
                column: "UnoGameId");

            migrationBuilder.CreateIndex(
                name: "IX_UnoGameDiscardPile_UnoGame1Id",
                table: "UnoGameDiscardPile",
                column: "UnoGame1Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnoGameDeck");

            migrationBuilder.DropTable(
                name: "UnoGameDiscardPile");

            migrationBuilder.AddColumn<int>(
                name: "UnoGameId",
                table: "Card",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnoGameId1",
                table: "Card",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Card_UnoGameId",
                table: "Card",
                column: "UnoGameId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_UnoGameId1",
                table: "Card",
                column: "UnoGameId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_UnoGame_UnoGameId",
                table: "Card",
                column: "UnoGameId",
                principalTable: "UnoGame",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_UnoGame_UnoGameId1",
                table: "Card",
                column: "UnoGameId1",
                principalTable: "UnoGame",
                principalColumn: "Id");
        }
    }
}
