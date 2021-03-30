using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoEngine.Migrations
{
    public partial class esh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "GameStates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameStates_GameId",
                table: "GameStates",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameStates_GameResults_GameId",
                table: "GameStates",
                column: "GameId",
                principalTable: "GameResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameStates_GameResults_GameId",
                table: "GameStates");

            migrationBuilder.DropIndex(
                name: "IX_GameStates_GameId",
                table: "GameStates");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "GameStates");
        }
    }
}
