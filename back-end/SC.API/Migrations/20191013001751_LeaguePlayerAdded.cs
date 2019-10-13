using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SC.API.Migrations
{
    public partial class LeaguePlayerAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaguePlayer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    ModifiedUserId = table.Column<Guid>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    LeagueId = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    Handicap = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaguePlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaguePlayer_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaguePlayer_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTournament_PlayerId",
                table: "PlayerTournament",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTournament_TournamentId",
                table: "PlayerTournament",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguePlayer_LeagueId",
                table: "LeaguePlayer",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguePlayer_PlayerId",
                table: "LeaguePlayer",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTournament_Players_PlayerId",
                table: "PlayerTournament",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTournament_Tournaments_TournamentId",
                table: "PlayerTournament",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTournament_Players_PlayerId",
                table: "PlayerTournament");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTournament_Tournaments_TournamentId",
                table: "PlayerTournament");

            migrationBuilder.DropTable(
                name: "LeaguePlayer");

            migrationBuilder.DropIndex(
                name: "IX_PlayerTournament_PlayerId",
                table: "PlayerTournament");

            migrationBuilder.DropIndex(
                name: "IX_PlayerTournament_TournamentId",
                table: "PlayerTournament");
        }
    }
}
