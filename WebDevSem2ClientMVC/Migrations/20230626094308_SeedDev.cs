using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevSem2ClientMVC.Migrations
{
    /// <inheritdoc />
    public partial class SeedDev : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeveloperProfile",
                columns: table => new
                {
                    DeveloperProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperProfile", x => x.DeveloperProfileId);
                });

            migrationBuilder.InsertData(
                table: "DeveloperProfile",
                columns: new[] { "DeveloperProfileId", "Discription", "Email", "Name", "PictureURL", "Skills" },
                values: new object[] { 1, "Mijn naam is Ingvar Schoenmaker en ik ben 23 jaar oud.\r\n Ik volg de opleiding HBO-ICT en heb de richting ontwikkeling gekozen.\r\n Momenteel zit ik in mijn laatste jaar maar heb voor een reparatiesimester gekozen.\r\n", "ingvar.schoenmaker@windesheim.nl", "Ingvar Schoenmaker", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.youtube.com%2Fuser%2Fingvarschoenmaker&psig=AOvVaw2ky-X0nW2dZBTyBvFecmhq&ust=1687858638390000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCPDm5MTR4P8CFQAAAAAdAAAAABAE", "Ik heb verschillende skills zoals c#, js en Python" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeveloperProfile");
        }
    }
}
