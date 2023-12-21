using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevSem2ClientMVC.Migrations
{
    /// <inheritdoc />
    public partial class testt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                table: "UnoGame");

            migrationBuilder.DropColumn(
                name: "test",
                table: "LobbyTable");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "test",
                table: "UnoGame",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "test",
                table: "LobbyTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
