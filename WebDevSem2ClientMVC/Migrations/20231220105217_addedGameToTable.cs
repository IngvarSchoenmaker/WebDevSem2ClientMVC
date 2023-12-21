using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevSem2ClientMVC.Migrations
{
    /// <inheritdoc />
    public partial class addedGameToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "LobbyTable",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LobbyTable_GameId",
                table: "LobbyTable",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_LobbyTable_UnoGame_GameId",
                table: "LobbyTable",
                column: "GameId",
                principalTable: "UnoGame",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LobbyTable_UnoGame_GameId",
                table: "LobbyTable");

            migrationBuilder.DropIndex(
                name: "IX_LobbyTable_GameId",
                table: "LobbyTable");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "LobbyTable");
        }
    }
}
