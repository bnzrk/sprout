using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sprout.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSrsAndKanjiModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Kanji_Literal",
                table: "Kanji");

            migrationBuilder.RenameColumn(
                name: "LastReviewed",
                table: "SrsData",
                newName: "LastReview");

            migrationBuilder.AlterColumn<string>(
                name: "Literal",
                table: "Kanji",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastReview",
                table: "SrsData",
                newName: "LastReviewed");

            migrationBuilder.AlterColumn<string>(
                name: "Literal",
                table: "Kanji",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Kanji_Literal",
                table: "Kanji",
                column: "Literal",
                unique: true);
        }
    }
}
