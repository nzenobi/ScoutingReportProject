using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace ScoutingReportModels
{
    public partial class League
    {
        public League()
        {
            TeamLeagueKeyDomesticNavigations = new HashSet<Team>();
            TeamLeagueKeyNavigations = new HashSet<Team>();
        }

        public int LeagueKey { get; set; }
        public string LeagueName { get; set; }
        public string Country { get; set; }
        public bool? ActiveSource { get; set; }
        public int? LeagueGroupKey { get; set; }
        public int? LeagueCustomGroupKey { get; set; }
        [JsonIgnore]
        public bool? SearchDisplayFlag { get; set; }
        [JsonIgnore]
        public virtual ICollection<Team> TeamLeagueKeyDomesticNavigations { get; set; }
        [JsonIgnore]
        public virtual ICollection<Team> TeamLeagueKeyNavigations { get; set; }
    }
}
