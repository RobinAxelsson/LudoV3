using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoEngine.Migrations
{
    public partial class ChangeGameIDInPawns : Migration
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
                name: "GameID",
                table: "Pawns",
                newName: "GameId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Pawns",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Pawns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Pawns_GameId",
                table: "Pawns",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pawns_Games_GameId",
                table: "Pawns",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pawns_Games_GameId",
                table: "Pawns");

            migrationBuilder.DropIndex(
                name: "IX_Pawns_GameId",
                table: "Pawns");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Pawns",
                newName: "GameID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pawns",
                newName: "ID");

            migrationBuilder.AlterColumn<int>(
                name: "GameID",
                table: "Pawns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
