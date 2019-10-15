using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SC.API.Migrations
{
    public partial class TournamentIdAndGroupIdAddedOnFrame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Frames",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TournamentId",
                table: "Frames",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupPlayer_GroupId",
                table: "GroupPlayer",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPlayer_PlayerId",
                table: "GroupPlayer",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Frames_GroupId",
                table: "Frames",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Frames_TournamentId",
                table: "Frames",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Frames_WinnerId",
                table: "Frames",
                column: "WinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Frames_Groups_GroupId",
                table: "Frames",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Frames_Tournaments_TournamentId",
                table: "Frames",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Frames_Players_WinnerId",
                table: "Frames",
                column: "WinnerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPlayer_Groups_GroupId",
                table: "GroupPlayer",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPlayer_Players_PlayerId",
                table: "GroupPlayer",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Frames_Groups_GroupId",
                table: "Frames");

            migrationBuilder.DropForeignKey(
                name: "FK_Frames_Tournaments_TournamentId",
                table: "Frames");

            migrationBuilder.DropForeignKey(
                name: "FK_Frames_Players_WinnerId",
                table: "Frames");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPlayer_Groups_GroupId",
                table: "GroupPlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPlayer_Players_PlayerId",
                table: "GroupPlayer");

            migrationBuilder.DropIndex(
                name: "IX_GroupPlayer_GroupId",
                table: "GroupPlayer");

            migrationBuilder.DropIndex(
                name: "IX_GroupPlayer_PlayerId",
                table: "GroupPlayer");

            migrationBuilder.DropIndex(
                name: "IX_Frames_GroupId",
                table: "Frames");

            migrationBuilder.DropIndex(
                name: "IX_Frames_TournamentId",
                table: "Frames");

            migrationBuilder.DropIndex(
                name: "IX_Frames_WinnerId",
                table: "Frames");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Frames");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "Frames");
        }
    }
}
