using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoEngine.Migrations
{
    public partial class SecondMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PawnSavePoints_Games_GameId",
                table: "PawnSavePoints");

            migrationBuilder.DropIndex(
                name: "IX_PawnSavePoints_GameId",
                table: "PawnSavePoints");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "PawnSavePoints",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "PawnSavePoints",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_PawnSavePoints_GameId",
                table: "PawnSavePoints",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_PawnSavePoints_Games_GameId",
                table: "PawnSavePoints",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
