using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace ScoutingReportModels
{
    public partial class User
    {
        public string AzureAdUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? ActiveFlag { get; set; }
        public int? Order { get; set; }

        [JsonIgnore]
        public virtual ICollection<ScoutingReport> ScoutingReports { get; set; }
    }
}
