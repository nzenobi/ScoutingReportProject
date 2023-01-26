using ScoutingReportDAL.Db;
using ScoutingReportDAL.Repositories;
using ScoutingReportModels;
using System.Collections.Generic;
using System.Linq;

namespace ScoutingReportServices
{
    public class PlayerService : IPlayerService
    {
        private readonly IScoutingReportRepository _scoutingReportRepository;
        public PlayerService(IScoutingReportRepository scoutingReportRepository)
        {
            _scoutingReportRepository = scoutingReportRepository;
        }


        public List<Player> GetPlayerList(ActivePlayerRequest activePlayerRequest)
        {
            var players = _scoutingReportRepository.GetPlayers(activePlayerRequest);
            return players;
        }
        
    }
}
