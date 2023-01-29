using System;
using System.Collections.Generic;

namespace ScoutingReportModels
{
    public class ScoutingReportResponse
    {
        public ScoutingReportResponse()
        {
            TeamPlayerReports = new List<ScoutingReportTeamResponse>();
            FreeAgentReports = new List<ScoutingReportsResponse>();
        }
        public List<ScoutingReportTeamResponse> TeamPlayerReports { get; set; }
        public List<ScoutingReportsResponse> FreeAgentReports { get; set; }
    }
    public class ScoutingReportTeamResponse
    {
        public int TeamId { get; set; }
        public string NickName { get; set; }
        public string Conference { get; set; }
        public List<PlayerScoutingReportResponse> Players { get; set; }
    }


    public class PlayerScoutingReportResponse
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string DOB { get; set; }
        public List<ScoutingReportsResponse> Reports { get; set; }
    }

    public class ScoutingReportsResponse
    {
        public Guid ScoutingReportId { get; set; }
        public string ScoutId { get; set; }
        public string CreatedDateTime { get; set; }
        public string ModifiedDateTime { get; set; }
        public string Comments { get; set; }
        public int Defense { get; set; }
        public int Rebound { get; set; }
        public int Shooting { get; set; }
    }
}
