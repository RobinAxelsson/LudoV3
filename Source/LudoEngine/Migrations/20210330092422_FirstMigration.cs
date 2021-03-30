using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoEngine.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstPlace = table.Column<int>(type: "int", nullable: false),
                    SecondPlace = table.Column<int>(type: "int", nullable: false),
                    ThirdPlace = table.Column<int>(type: "int", nullable: false),
                    ForthPlace = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentPlayer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pawn",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false),
                    XPosition = table.Column<int>(type: "int", nullable: false),
                    YPosition = table.Column<int>(type: "int", nullable: false),
                    GameStateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pawn", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pawn_GameStates_GameStateId",
                        column: x => x.GameStateId,
                        principalTable: "GameStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameStateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_GameStates_GameStateId",
                        column: x => x.GameStateId,
                        principalTable: "GameStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pawn_GameStateId",
                table: "Pawn",
                column: "GameStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameStateId",
                table: "Players",
                column: "GameStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameResults");

            migrationBuilder.DropTable(
                name: "Pawn");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "GameStates");
        }
    }
}
