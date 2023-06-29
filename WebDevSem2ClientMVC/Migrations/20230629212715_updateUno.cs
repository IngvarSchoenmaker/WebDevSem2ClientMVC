using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebDevSem2ClientMVC.Migrations
{
    /// <inheritdoc />
    public partial class updateUno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardColors",
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
                nullable: false,
                defaultValue: 0);

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
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);
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
