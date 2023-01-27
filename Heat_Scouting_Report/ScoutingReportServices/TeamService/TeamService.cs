using Microsoft.Extensions.Logging;
using ScoutingReportDAL.Repositories;
using ScoutingReportModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingReportServices.TeamService
{
    public class TeamService : ITeamService
    {
        private readonly IScoutingReportRepository _scoutingReportRepository;
        private readonly ILogger<TeamService> _logger;

        public TeamService(IScoutingReportRepository scoutingReportRepository, ILogger<TeamService> logger)
        {
            _scoutingReportRepository = scoutingReportRepository;
            _logger = logger;
        }

        public async Task<List<Team>> GetTeamsByLeagueId(int leagueId)
        {
            try
            {
                List<Team> teams = await _scoutingReportRepository.GetTeams(leagueId);
                return teams;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error fetching teams by league ID", ex);
                return null;
            }
        }
    }
}
