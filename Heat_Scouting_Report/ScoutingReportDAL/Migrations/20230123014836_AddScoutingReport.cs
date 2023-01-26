using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Heat_Scouting_Report.Migrations
{
    public partial class AddScoutingReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScoutingReports",
                columns: table => new
                {
                    ScoutingReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScoutId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    Rebound = table.Column<int>(type: "int", nullable: false),
                    Shooting = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoutingReports", x => x.ScoutingReportId);
                    table.ForeignKey(
                        name: "FK_ScoutingReports_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoutingReports_User_ScoutId",
                        column: x => x.ScoutId,
                        principalTable: "User",
                        principalColumn: "AzureAdUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScoutingReports_PlayerId",
                table: "ScoutingReports",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoutingReports_ScoutId",
                table: "ScoutingReports",
                column: "ScoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoutingReports");
        }
    }
}
