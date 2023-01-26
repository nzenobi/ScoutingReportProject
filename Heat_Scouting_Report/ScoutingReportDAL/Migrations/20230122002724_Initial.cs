using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Heat_Scouting_Report.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "League",
                columns: table => new
                {
                    LeagueKey = table.Column<int>(type: "int", nullable: false),
                    LeagueName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ActiveSource = table.Column<bool>(type: "bit", nullable: true),
                    LeagueGroupKey = table.Column<int>(type: "int", nullable: true),
                    LeagueCustomGroupKey = table.Column<int>(type: "int", nullable: true),
                    SearchDisplayFlag = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League", x => x.LeagueKey);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerKey = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true),
                    PositionKey = table.Column<int>(type: "int", nullable: true),
                    AgentKey = table.Column<int>(type: "int", nullable: true),
                    FreeAgentYear = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(6,4)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    YearsOfService = table.Column<int>(type: "int", nullable: true),
                    Wing = table.Column<decimal>(type: "decimal(6,4)", nullable: true),
                    BodyFat = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    StandingReach = table.Column<decimal>(type: "decimal(6,4)", nullable: true),
                    CourtRunTime_3_4 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    VerticalJumpNoStep = table.Column<decimal>(type: "decimal(6,4)", nullable: true),
                    VerticalJumpMax = table.Column<decimal>(type: "decimal(6,4)", nullable: true),
                    HandWidth = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    HandLength = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    URLPhoto = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ActiveAnalysisFlg = table.Column<bool>(type: "bit", nullable: false),
                    LeagueCustomGroupKey = table.Column<int>(type: "int", nullable: true),
                    BboPlayerKey = table.Column<int>(type: "int", nullable: true),
                    dwh_insert_datetime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    dwh_update_datetime = table.Column<DateTime>(type: "datetime", nullable: true),
                    AgentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AgentPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CommittedTo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Handedness = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GLPlayerKey = table.Column<int>(type: "int", nullable: true),
                    PlayerStatusKey = table.Column<int>(type: "int", nullable: true),
                    Height_Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Weight_Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Wing_Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BodyFat_Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StandingReach_Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CourtRunTime_3_4_Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VerticalJumpNoStep_Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VerticalJumpMax_Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Hand_W_H_Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Hand = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsCustomData = table.Column<bool>(type: "bit", nullable: false),
                    Handedness_Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerKey);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    AzureAdUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__76BABBB68573D222", x => x.AzureAdUserId);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamKey = table.Column<int>(type: "int", nullable: false),
                    LeagueKey = table.Column<int>(type: "int", nullable: true),
                    LeagueKey_Domestic = table.Column<int>(type: "int", nullable: true),
                    ArenaKey = table.Column<int>(type: "int", nullable: true),
                    TeamName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TeamNickname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Conference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SubConference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TeamCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TeamCountry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CoachName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    URLPhoto = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CurrentNBATeamFlg = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamKey);
                    table.ForeignKey(
                        name: "FK_Team_League",
                        column: x => x.LeagueKey,
                        principalTable: "League",
                        principalColumn: "LeagueKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Team_LeagueDomestic",
                        column: x => x.LeagueKey_Domestic,
                        principalTable: "League",
                        principalColumn: "LeagueKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamPlayer",
                columns: table => new
                {
                    PlayerKey = table.Column<int>(type: "int", nullable: false),
                    TeamKey = table.Column<int>(type: "int", nullable: false),
                    SeasonKey = table.Column<int>(type: "int", nullable: false),
                    ActiveTeamFlg = table.Column<bool>(type: "bit", nullable: true),
                    dwh_insert_datetime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayer", x => new { x.PlayerKey, x.TeamKey, x.SeasonKey });
                    table.ForeignKey(
                        name: "FK_TeamPlayer_Player",
                        column: x => x.PlayerKey,
                        principalTable: "Player",
                        principalColumn: "PlayerKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamPlayer_Team",
                        column: x => x.TeamKey,
                        principalTable: "Team",
                        principalColumn: "TeamKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Team_LeagueKey",
                table: "Team",
                column: "LeagueKey");

            migrationBuilder.CreateIndex(
                name: "IX_Team_LeagueKey_Domestic",
                table: "Team",
                column: "LeagueKey_Domestic");

            migrationBuilder.CreateIndex(
                name: "UK_DimTeam",
                table: "Team",
                columns: new[] { "TeamKey", "LeagueKey" },
                unique: true,
                filter: "[LeagueKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_TeamKey",
                table: "TeamPlayer",
                column: "TeamKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamPlayer");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "League");
        }
    }
}
