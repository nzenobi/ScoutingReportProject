using ScoutingReportDAL.Db;
using ScoutingReportModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScoutingReportServices
{
    public interface IPlayerService
    {
        Task<ActivePlayerResponse> GetPlayerList(ActivePlayerRequest activePlayerRequest);
        Task<List<Player>> GetRoster(RosterRequest rosterRequest);
    }
}
