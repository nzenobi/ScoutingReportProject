using ScoutingReportModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingReportServices.LeagueService
{
    public interface ILeagueService
    {
        Task<List<League>> GetLeagues();
    }
}
