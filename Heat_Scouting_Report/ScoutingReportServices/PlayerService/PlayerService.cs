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

        public async Task<ActivePlayerResponse> GetPlayerList(ActivePlayerRequest activePlayerRequest)
        {
            try
            {
                ActivePlayerResponse activePlayerResponses = new ActivePlayerResponse();
                List<Player> players = await _scoutingReportRepository.GetPlayers(activePlayerRequest);

                if(players != null)
                {
                    activePlayerResponses.ActivePlayers = new List<ActivePlayer>();
                    foreach (Player player in players)
                    {
                        ActivePlayer activePlayer = new ActivePlayer();
                        activePlayer.Player = player;

                        activePlayer.TeamList = player.TeamPlayers.GroupBy(tp => tp.SeasonKey).ToDictionary(tp => tp.Key, tp => tp.Select(tp => tp.TeamKeyNavigation).ToList());

                        activePlayerResponses.ActivePlayers.Add(activePlayer);
                    }
                }

                return activePlayerResponses;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching active players", ex);
                return null;
            }
        }
        
    }
}
