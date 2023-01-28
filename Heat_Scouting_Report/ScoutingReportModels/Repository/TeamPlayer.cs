using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace ScoutingReportModels
{
    public partial class TeamPlayer
    {
        public int PlayerKey { get; set; }
        public int TeamKey { get; set; }
        public int SeasonKey { get; set; }
        public bool? ActiveTeamFlg { get; set; }
        public DateTime DwhInsertDatetime { get; set; }

        [JsonIgnore]
        public virtual Player PlayerKeyNavigation { get; set; }
        [JsonIgnore]
        public virtual Team TeamKeyNavigation { get; set; }
    }
}
