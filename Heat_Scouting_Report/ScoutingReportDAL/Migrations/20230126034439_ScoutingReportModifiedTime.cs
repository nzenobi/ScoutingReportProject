using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Heat_Scouting_Report.Migrations
{
    public partial class ScoutingReportModifiedTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "ScoutingReports",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "ScoutingReports");
        }
    }
}
