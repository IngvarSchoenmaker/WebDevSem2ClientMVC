using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevSem2ClientMVC.Migrations
{
    /// <inheritdoc />
    public partial class addedGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UnoGame",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameStatus = table.Column<int>(type: "int", nullable: false),
                    CurrentPlayerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnoGame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnoGame_AspNetUsers_CurrentPlayerId",
                        column: x => x.CurrentPlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UnoGameId = table.Column<int>(type: "int", nullable: true),
                    UnoGameId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Card_UnoGame_UnoGameId",
                        column: x => x.UnoGameId,
                        principalTable: "UnoGame",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Card_UnoGame_UnoGameId1",
                        column: x => x.UnoGameId1,
                        principalTable: "UnoGame",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GameId",
                table: "AspNetUsers",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_PlayerId",
                table: "Card",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_UnoGameId",
                table: "Card",
                column: "UnoGameId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_UnoGameId1",
                table: "Card",
                column: "UnoGameId1");

            migrationBuilder.CreateIndex(
                name: "IX_UnoGame_CurrentPlayerId",
                table: "UnoGame",
                column: "CurrentPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UnoGame_GameId",
                table: "AspNetUsers",
                column: "GameId",
                principalTable: "UnoGame",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UnoGame_GameId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "UnoGame");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GameId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");
        }
    }
}
