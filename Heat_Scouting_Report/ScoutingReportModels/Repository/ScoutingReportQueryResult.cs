using System;

namespace ScoutingReportModels
{
    public class ScoutingReportQueryResult
    {
        public ScoutingReport ScoutingReport { get; set; }
        public Team Team { get; set; }
        public TeamPlayer TeamPlayer { get; set; }
        public Player Player { get; set; }
    }
}
