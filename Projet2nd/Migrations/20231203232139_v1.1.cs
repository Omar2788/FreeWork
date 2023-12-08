using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projet2nd.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    IdService = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descriptionService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prixService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imageService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    etatService = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.IdService);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
