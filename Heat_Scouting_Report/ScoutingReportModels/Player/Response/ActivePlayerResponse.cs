using System;
using System.Collections.Generic;
using System.Text;

namespace ScoutingReportModels
{
    public class ActivePlayerResponse
    {
        public List<ActivePlayer> ActivePlayers { get; set; }
    }

    public class ActivePlayer
    { 
        public Player Player { get; set; }
        public Dictionary<int, List<Team>> TeamList { get; set; }
    }

}
