using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevSem2API.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactFormModel",
                columns: table => new
                {
                    ContactFormId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeveloperProfileId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactFormModel", x => x.ContactFormId);
                });

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
                values: new object[] { 1, "Mijn naam is Ingvar Schoenmaker en ik ben 23 jaar oud.\r\n Ik volg de opleiding HBO-ICT en heb de richting ontwikkeling gekozen.\r\n Momenteel zit ik in mijn laatste jaar maar heb voor een reparatiesimester gekozen.\r\n", "ingvar.schoenmaker@windesheim.nl", "Ingvar Schoenmaker", "https://media.licdn.com/dms/image/C5603AQGU4RhjRZQnxg/profile-displayphoto-shrink_800_800/0/1517319432686?e=1681344000&v=beta&t=F6_63VvXX5m6Vu3q0UfMy89AnpCZuCnPyTM64UleLs8", "Ik heb verschillende skills zoals c#, js en Python" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactFormModel");

            migrationBuilder.DropTable(
                name: "DeveloperProfile");
        }
    }
}
