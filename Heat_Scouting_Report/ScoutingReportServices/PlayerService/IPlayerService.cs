using ScoutingReportDAL.Db;
using ScoutingReportModels;
using System;
using System.Collections.Generic;

namespace ScoutingReportServices
{
    public interface IPlayerService
    {
        List<Player> GetPlayerList(ActivePlayerRequest activePlayerRequest);
    }
}
