using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevSem2ClientMVC.Migrations
{
    /// <inheritdoc />
    public partial class ConnectedApplicationuserToPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Player_PlayerId",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Player",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Card",
                newName: "Playerid");

            migrationBuilder.RenameIndex(
                name: "IX_Card_PlayerId",
                table: "Card",
                newName: "IX_Card_Playerid");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Player",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_UserId",
                table: "Player",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Player_Playerid",
                table: "Card",
                column: "Playerid",
                principalTable: "Player",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_AspNetUsers_UserId",
                table: "Player",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Player_Playerid",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_AspNetUsers_UserId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_UserId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Player",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Playerid",
                table: "Card",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Card_Playerid",
                table: "Card",
                newName: "IX_Card_PlayerId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Player",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Player_PlayerId",
                table: "Card",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id");
        }
    }
}
