using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevSem2ClientMVC.Migrations
{
    /// <inheritdoc />
    public partial class removedPlayerFromIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UnoGame_GameId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_AspNetUsers_PlayerId",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GameId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Card",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_UnoGame_GameId",
                        column: x => x.GameId,
                        principalTable: "UnoGame",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_GameId",
                table: "Player",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Player_PlayerId",
                table: "Card",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Player_PlayerId",
                table: "Card");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.AlterColumn<string>(
                name: "PlayerId",
                table: "Card",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GameId",
                table: "AspNetUsers",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UnoGame_GameId",
                table: "AspNetUsers",
                column: "GameId",
                principalTable: "UnoGame",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_AspNetUsers_PlayerId",
                table: "Card",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
