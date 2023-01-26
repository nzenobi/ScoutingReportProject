using System;
using System.Collections.Generic;

#nullable disable

namespace ScoutingReportModels
{
    public partial class ScoutingReport
    {
        public Guid ScoutingReportId { get; set; }
        public string ScoutId { get; set; }
        public int PlayerId { get; set; }
        public string Comments { get; set; }
        public int Defense { get; set; }
        public int Rebound { get; set; }
        public int Shooting { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public bool IsActive { get; set; }
        public virtual Player Player { get; set; }
        public virtual User Scout { get; set; }
    }
}
