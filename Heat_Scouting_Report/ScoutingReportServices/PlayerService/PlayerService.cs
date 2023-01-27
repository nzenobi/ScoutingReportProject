using Microsoft.Extensions.Logging;
using ScoutingReportDAL.Db;
using ScoutingReportDAL.Repositories;
using ScoutingReportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutingReportServices
{
    public class PlayerService : IPlayerService
    {
        private readonly IScoutingReportRepository _scoutingReportRepository;
        private readonly ILogger<PlayerService> _logger;

        public PlayerService(IScoutingReportRepository scoutingReportRepository, ILogger<PlayerService> logger)
        {
            _scoutingReportRepository = scoutingReportRepository;
            _logger = logger;
        }

        public async Task<List<Player>> GetRoster(RosterRequest rosterRequest)
        {
            try
            {
                List<Player> roster = await _scoutingReportRepository.GetRoster(rosterRequest);
                return roster;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error fetching roster", ex);
                return null;
            }
        }

        public List<Player> GetPlayerList(ActivePlayerRequest activePlayerRequest)
        {
            var players = _scoutingReportRepository.GetPlayers(activePlayerRequest);
            return players;
        }
        
    }
}
