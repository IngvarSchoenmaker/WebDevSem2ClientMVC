using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevSem2ClientMVC.Migrations
{
    /// <inheritdoc />
    public partial class unoupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardColor",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CardValue",
                table: "Cards");

            migrationBuilder.AlterColumn<int>(
                name: "GameStatus",
                table: "Game",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CurrentCardCardId",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Game_CurrentCardCardId",
                table: "Game",
                column: "CurrentCardCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardId",
                table: "Cards",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Card_CardId",
                table: "Cards",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Card_CurrentCardCardId",
                table: "Game",
                column: "CurrentCardCardId",
                principalTable: "Card",
                principalColumn: "CardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Card_CardId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Card_CurrentCardCardId",
                table: "Game");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Game_CurrentCardCardId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CardId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CurrentCardCardId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Cards");

            migrationBuilder.AlterColumn<string>(
                name: "GameStatus",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CardColors",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CardValue",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
