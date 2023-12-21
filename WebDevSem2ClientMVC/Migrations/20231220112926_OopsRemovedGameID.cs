using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevSem2ClientMVC.Migrations
{
    /// <inheritdoc />
    public partial class OopsRemovedGameID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnoGame_AspNetUsers_CurrentPlayerId",
                table: "UnoGame");

            migrationBuilder.DropIndex(
                name: "IX_UnoGame_CurrentPlayerId",
                table: "UnoGame");

            migrationBuilder.DropColumn(
                name: "CurrentPlayerId",
                table: "UnoGame");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentPlayerId",
                table: "UnoGame",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UnoGame_CurrentPlayerId",
                table: "UnoGame",
                column: "CurrentPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnoGame_AspNetUsers_CurrentPlayerId",
                table: "UnoGame",
                column: "CurrentPlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
