using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoEngine.Migrations
{
    public partial class FixingForeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pawns_GameID",
                table: "Pawns",
                column: "GameID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pawns_Games_GameID",
                table: "Pawns",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pawns_Games_GameID",
                table: "Pawns");

            migrationBuilder.DropIndex(
                name: "IX_Pawns_GameID",
                table: "Pawns");
        }
    }
}
