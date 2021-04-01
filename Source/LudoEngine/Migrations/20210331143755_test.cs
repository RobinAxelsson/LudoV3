using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoEngine.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pawns_Games_Game",
                table: "Pawns");

            migrationBuilder.DropIndex(
                name: "IX_Pawns_Game",
                table: "Pawns");

            migrationBuilder.DropColumn(
                name: "Game",
                table: "Pawns");

            migrationBuilder.AddColumn<int>(
                name: "GameID",
                table: "Pawns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pawns_GameID",
                table: "Pawns",
                column: "GameID");

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

            migrationBuilder.DropColumn(
                name: "GameID",
                table: "Pawns");

            migrationBuilder.AddColumn<int>(
                name: "Game",
                table: "Pawns",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pawns_Game",
                table: "Pawns",
                column: "Game");

            migrationBuilder.AddForeignKey(
                name: "FK_Pawns_Games_Game",
                table: "Pawns",
                column: "Game",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
