using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoEngine.Migrations
{
    public partial class RemoveFieldFromPawnSavePosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PawnId",
                table: "PawnSavePoints");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PawnId",
                table: "PawnSavePoints",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
