using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoEngine.Migrations
{
    public partial class fixingTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pawns_Games_GameID",
                table: "Pawns");

            migrationBuilder.DropIndex(
                name: "IX_Pawns_GameID",
                table: "Pawns");

            migrationBuilder.RenameColumn(
                name: "ForthPlace",
                table: "Games",
                newName: "FourthPlace");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FourthPlace",
                table: "Games",
                newName: "ForthPlace");

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
    }
}
