using System;

namespace ScoutingReportModels
{
    public class ScoutingReportRequest
    {
        public string ScoutId { get; set; }
        public int PlayerId { get; set; }
        public string Comments { get; set; }
        public int Defense { get; set; }
        public int Rebound { get; set; }
        public int Shooting { get; set; }
    }
}
