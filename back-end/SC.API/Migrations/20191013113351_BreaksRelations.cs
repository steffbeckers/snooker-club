using Microsoft.EntityFrameworkCore.Migrations;

namespace SC.API.Migrations
{
    public partial class BreaksRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Groups_TournamentId",
                table: "Groups",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Breaks_FrameId",
                table: "Breaks",
                column: "FrameId");

            migrationBuilder.CreateIndex(
                name: "IX_Breaks_PlayerId",
                table: "Breaks",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Breaks_Frames_FrameId",
                table: "Breaks",
                column: "FrameId",
                principalTable: "Frames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Breaks_Players_PlayerId",
                table: "Breaks",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Tournaments_TournamentId",
                table: "Groups",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breaks_Frames_FrameId",
                table: "Breaks");

            migrationBuilder.DropForeignKey(
                name: "FK_Breaks_Players_PlayerId",
                table: "Breaks");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Tournaments_TournamentId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_TournamentId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Breaks_FrameId",
                table: "Breaks");

            migrationBuilder.DropIndex(
                name: "IX_Breaks_PlayerId",
                table: "Breaks");
        }
    }
}
