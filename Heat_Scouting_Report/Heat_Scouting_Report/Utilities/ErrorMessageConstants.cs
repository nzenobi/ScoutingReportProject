using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heat_Scouting_Report.Utilities
{
    public class ErrorMessageConstants
    {
        public static string ActiveLeagues = "Error getting active leagues";
        public static string Roster = "Error fetching roster";
        public static string Players = "Error fetching players";
        public static string UpdateScoutingReport = "Error updating scouting report";
        public static string DeleteScoutingReport = "Error deleting scouting report";
        public static string CreateScoutingReport = "Error creating scouting report";
        public static string GetScoutingReport = "Error fetching scouting reports";
        public static string GetLeague = "Error getting teams by league Id";
        public static string ActiveScouts = "Error fetching active scouts";
    }
}
