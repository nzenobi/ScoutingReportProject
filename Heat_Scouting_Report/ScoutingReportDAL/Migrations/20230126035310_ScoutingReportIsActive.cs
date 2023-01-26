using Microsoft.EntityFrameworkCore.Migrations;

namespace Heat_Scouting_Report.Migrations
{
    public partial class ScoutingReportIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ScoutingReports",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ScoutingReports");
        }
    }
}
