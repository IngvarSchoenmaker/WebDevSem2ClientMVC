using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebDevSem2ClientMVC.Migrations
{
    /// <inheritdoc />
    public partial class Cardlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "CardId",
                keyValue: 36);
        }
    }
}
