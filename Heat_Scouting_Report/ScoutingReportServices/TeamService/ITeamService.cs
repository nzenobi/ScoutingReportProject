using ScoutingReportModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingReportServices.TeamService
{
    public interface ITeamService
    {
        Task<List<Team>> GetTeamsByLeagueId(int leagueId);
    }
}
