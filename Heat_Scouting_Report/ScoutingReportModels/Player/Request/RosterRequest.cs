using System;

namespace ScoutingReportModels
{
    public class RosterRequest
    {
        public int TeamId { get; set; }
        public int SeasonId { get; set; }
        public bool ActiveOnly { get; set; }
    }
}
