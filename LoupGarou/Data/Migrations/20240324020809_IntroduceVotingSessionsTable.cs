using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoupGarou.Data.Migrations
{
    /// <inheritdoc />
    public partial class IntroduceVotingSessionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Games_GameId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "Votes");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Votes",
                newName: "VotingSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_GameId",
                table: "Votes",
                newName: "IX_Votes_VotingSessionId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "VotingSessions",
                columns: table => new
                {
                    VotingSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VotingSessionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Result = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotingSessions", x => x.VotingSessionId);
                    table.ForeignKey(
                        name: "FK_VotingSessions_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VotingSessions_GameId",
                table: "VotingSessions",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_VotingSessions_VotingSessionId",
                table: "Votes",
                column: "VotingSessionId",
                principalTable: "VotingSessions",
                principalColumn: "VotingSessionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_VotingSessions_VotingSessionId",
                table: "Votes");

            migrationBuilder.DropTable(
                name: "VotingSessions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "VotingSessionId",
                table: "Votes",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_VotingSessionId",
                table: "Votes",
                newName: "IX_Votes_GameId");

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Games_GameId",
                table: "Votes",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
