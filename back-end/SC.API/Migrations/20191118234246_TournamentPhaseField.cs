using Microsoft.EntityFrameworkCore.Migrations;

namespace SC.API.Migrations
{
    public partial class TournamentPhaseField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TournamentPhase",
                table: "Frames",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TournamentPhase",
                table: "Frames");
        }
    }
}
