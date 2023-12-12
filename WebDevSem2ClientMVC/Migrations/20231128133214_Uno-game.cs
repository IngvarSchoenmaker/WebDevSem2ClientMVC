using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebDevSem2ClientMVC.Migrations
{
    /// <inheritdoc />
    public partial class Unogame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Game_GameId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GameId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardColor = table.Column<int>(type: "int", nullable: false),
                    CardValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.CardId);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentCardCardId = table.Column<int>(type: "int", nullable: true),
                    GameName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GameStatus = table.Column<int>(type: "int", nullable: false),
                    NumberOfPlayers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Game_Card_CurrentCardCardId",
                        column: x => x.CurrentCardCardId,
                        principalTable: "Card",
                        principalColumn: "CardId");
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardsId);
                    table.ForeignKey(
                        name: "FK_Cards_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cards_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Card",
                columns: new[] { "CardId", "CardColor", "CardValue" },
                values: new object[,]
                {
                    { 1, 1, "1" },
                    { 2, 2, "1" },
                    { 3, 3, "1" },
                    { 4, 4, "1" },
                    { 5, 1, "2" },
                    { 6, 2, "2" },
                    { 7, 3, "2" },
                    { 8, 4, "2" },
                    { 9, 1, "3" },
                    { 10, 2, "3" },
                    { 11, 3, "3" },
                    { 12, 4, "3" },
                    { 13, 1, "4" },
                    { 14, 2, "4" },
                    { 15, 3, "4" },
                    { 16, 4, "4" },
                    { 17, 1, "5" },
                    { 18, 2, "5" },
                    { 19, 3, "5" },
                    { 20, 4, "5" },
                    { 21, 1, "6" },
                    { 22, 2, "6" },
                    { 23, 3, "6" },
                    { 24, 4, "6" },
                    { 25, 1, "7" },
                    { 26, 2, "7" },
                    { 27, 3, "7" },
                    { 28, 4, "7" },
                    { 29, 1, "8" },
                    { 30, 2, "8" },
                    { 31, 3, "8" },
                    { 32, 4, "8" },
                    { 33, 1, "9" },
                    { 34, 2, "9" },
                    { 35, 3, "9" },
                    { 36, 4, "9" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GameId",
                table: "AspNetUsers",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardId",
                table: "Cards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_GameId",
                table: "Cards",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_PlayerId",
                table: "Cards",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_CurrentCardCardId",
                table: "Game",
                column: "CurrentCardCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Game_GameId",
                table: "AspNetUsers",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "GameId");
        }
    }
}
