using System;
using System.Collections.Generic;

namespace ScoutingReportModels
{
    public class ScoutingReportResponse
    {
        public int teamId { get; set; }
        public string nickName { get; set; }
        public string conference { get; set; }
        public List<PlayerScoutingReportResponse> players { get; set; }
    }


    public class PlayerScoutingReportResponse
    {
        public int playerId { get; set; }
        public string playerName { get; set; }
        public string dob { get; set; }
        public List<ScoutingReportsResponse> reports { get; set; }
    }

    public class ScoutingReportsResponse
    {
        public Guid ScoutingReportId { get; set; }
        public string scoutId { get; set; }
        public string createdDateTime { get; set; }
        public string ModifiedDateTime { get; set; }
        public string comments { get; set; }
        public int defense { get; set; }
        public int rebound { get; set; }
        public int shooting { get; set; }
    }
}
