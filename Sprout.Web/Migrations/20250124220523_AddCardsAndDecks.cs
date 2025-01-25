using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sprout.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddCardsAndDecks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Literal",
                table: "Kanji",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Kanji = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SrsData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    ProgressLevel = table.Column<int>(type: "int", nullable: false),
                    FirstReview = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastReviewed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextReview = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsMastered = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SrsData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SrsData_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardDeck",
                columns: table => new
                {
                    CardsId = table.Column<int>(type: "int", nullable: false),
                    DecksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardDeck", x => new { x.CardsId, x.DecksId });
                    table.ForeignKey(
                        name: "FK_CardDeck_Cards_CardsId",
                        column: x => x.CardsId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardDeck_Decks_DecksId",
                        column: x => x.DecksId,
                        principalTable: "Decks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kanji_Literal",
                table: "Kanji",
                column: "Literal",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardDeck_DecksId",
                table: "CardDeck",
                column: "DecksId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_Kanji_UserId",
                table: "Cards",
                columns: new[] { "Kanji", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SrsData_CardId",
                table: "SrsData",
                column: "CardId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardDeck");

            migrationBuilder.DropTable(
                name: "SrsData");

            migrationBuilder.DropTable(
                name: "Decks");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Kanji_Literal",
                table: "Kanji");

            migrationBuilder.AlterColumn<string>(
                name: "Literal",
                table: "Kanji",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
