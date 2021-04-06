using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoEngine.Migrations
{
    public partial class FirstBestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GamePlayers",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayers", x => new { x.GameId, x.PlayerId });
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentTurn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstPlace = table.Column<int>(type: "int", nullable: false),
                    SecondPlace = table.Column<int>(type: "int", nullable: false),
                    ThirdPlace = table.Column<int>(type: "int", nullable: false),
                    FourthPlace = table.Column<int>(type: "int", nullable: false),
                    PlayerGameGameId = table.Column<int>(type: "int", nullable: true),
                    PlayerGamePlayerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_GamePlayers_PlayerGameGameId_PlayerGamePlayerId",
                        columns: x => new { x.PlayerGameGameId, x.PlayerGamePlayerId },
                        principalTable: "GamePlayers",
                        principalColumns: new[] { "GameId", "PlayerId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerGameGameId = table.Column<int>(type: "int", nullable: true),
                    PlayerGamePlayerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_GamePlayers_PlayerGameGameId_PlayerGamePlayerId",
                        columns: x => new { x.PlayerGameGameId, x.PlayerGamePlayerId },
                        principalTable: "GamePlayers",
                        principalColumns: new[] { "GameId", "PlayerId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pawns",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameID = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false),
                    XPosition = table.Column<int>(type: "int", nullable: false),
                    YPosition = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pawns", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pawns_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlayer",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayer", x => new { x.GameId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_GamePlayer_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlayer_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayer_PlayerId",
                table: "GamePlayer",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerGameGameId_PlayerGamePlayerId",
                table: "Games",
                columns: new[] { "PlayerGameGameId", "PlayerGamePlayerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Pawns_GameID",
                table: "Pawns",
                column: "GameID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_PlayerGameGameId_PlayerGamePlayerId",
                table: "Players",
                columns: new[] { "PlayerGameGameId", "PlayerGamePlayerId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlayer");

            migrationBuilder.DropTable(
                name: "Pawns");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "GamePlayers");
        }
    }
}
