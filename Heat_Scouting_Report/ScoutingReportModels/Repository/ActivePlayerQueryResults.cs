using System;
using System.Collections.Generic;
using System.Text;

namespace ScoutingReportModels.Repository
{
    public class ActivePlayerQueryResults
    {
        public Team Team { get; set; }
        public TeamPlayer TeamPlayer { get; set; }
        public Player Player { get; set; }
    }
}
