using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoEngine.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameStatePawn");

            migrationBuilder.DropTable(
                name: "GameStatePlayer");

            migrationBuilder.AddColumn<int>(
                name: "PawnID",
                table: "GameStates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "GameStates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameStates_PawnID",
                table: "GameStates",
                column: "PawnID");

            migrationBuilder.CreateIndex(
                name: "IX_GameStates_PlayerId",
                table: "GameStates",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameStates_Pawn_PawnID",
                table: "GameStates",
                column: "PawnID",
                principalTable: "Pawn",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameStates_Players_PlayerId",
                table: "GameStates",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameStates_Pawn_PawnID",
                table: "GameStates");

            migrationBuilder.DropForeignKey(
                name: "FK_GameStates_Players_PlayerId",
                table: "GameStates");

            migrationBuilder.DropIndex(
                name: "IX_GameStates_PawnID",
                table: "GameStates");

            migrationBuilder.DropIndex(
                name: "IX_GameStates_PlayerId",
                table: "GameStates");

            migrationBuilder.DropColumn(
                name: "PawnID",
                table: "GameStates");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "GameStates");

            migrationBuilder.CreateTable(
                name: "GameStatePawn",
                columns: table => new
                {
                    GameStatesId = table.Column<int>(type: "int", nullable: false),
                    PawnsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatePawn", x => new { x.GameStatesId, x.PawnsID });
                    table.ForeignKey(
                        name: "FK_GameStatePawn_GameStates_GameStatesId",
                        column: x => x.GameStatesId,
                        principalTable: "GameStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStatePawn_Pawn_PawnsID",
                        column: x => x.PawnsID,
                        principalTable: "Pawn",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameStatePlayer",
                columns: table => new
                {
                    GameStatesId = table.Column<int>(type: "int", nullable: false),
                    PlayersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatePlayer", x => new { x.GameStatesId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_GameStatePlayer_GameStates_GameStatesId",
                        column: x => x.GameStatesId,
                        principalTable: "GameStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStatePlayer_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameStatePawn_PawnsID",
                table: "GameStatePawn",
                column: "PawnsID");

            migrationBuilder.CreateIndex(
                name: "IX_GameStatePlayer_PlayersId",
                table: "GameStatePlayer",
                column: "PlayersId");
        }
    }
}
