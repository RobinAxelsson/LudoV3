using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoEngine.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pawn_GameStates_GameStateId",
                table: "Pawn");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_GameStates_GameStateId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_GameStateId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Pawn_GameStateId",
                table: "Pawn");

            migrationBuilder.DropColumn(
                name: "GameStateId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GameStateId",
                table: "Pawn");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameStatePawn");

            migrationBuilder.DropTable(
                name: "GameStatePlayer");

            migrationBuilder.AddColumn<int>(
                name: "GameStateId",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameStateId",
                table: "Pawn",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameStateId",
                table: "Players",
                column: "GameStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Pawn_GameStateId",
                table: "Pawn",
                column: "GameStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pawn_GameStates_GameStateId",
                table: "Pawn",
                column: "GameStateId",
                principalTable: "GameStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_GameStates_GameStateId",
                table: "Players",
                column: "GameStateId",
                principalTable: "GameStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
