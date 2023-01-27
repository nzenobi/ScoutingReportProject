using Microsoft.Extensions.Logging;
using ScoutingReportDAL.Repositories;
using ScoutingReportModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingReportServices.LeagueService
{
    public class LeagueService : ILeagueService
    {
        private readonly IScoutingReportRepository _scoutingReportRepository;
        private readonly ILogger<LeagueService> _logger;

        public LeagueService(IScoutingReportRepository scoutingReportRepository, ILogger<LeagueService> logger)
        {
            _scoutingReportRepository = scoutingReportRepository;
            _logger = logger;
        }

        public async Task<List<League>> GetLeagues()
        {
            try
            {
                List<League> leagues = await _scoutingReportRepository.GetLeagues();
                return leagues;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching leagues", ex);
                return null;
            }
        }
    }
}
