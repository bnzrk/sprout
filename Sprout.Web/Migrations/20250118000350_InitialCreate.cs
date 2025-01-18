using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sprout.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kanji",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Literal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Meanings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KunReadings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnReadings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NanoriReadings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: true),
                    JLPTLevel = table.Column<int>(type: "int", nullable: true),
                    Frequency = table.Column<int>(type: "int", nullable: true),
                    StrokeCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kanji", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kanji");
        }
    }
}
