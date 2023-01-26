﻿
using ScoutingReportDAL.Db;
using ScoutingReportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutingReportDAL.Repositories
{
    public interface IScoutingReportRepository
    {
        Task<List<User>> GetActiveScouts();
        List<Player> GetPlayers(ActivePlayerRequest activePlayerRequest);
        Task CreateScoutingReport(ScoutingReport scoutingReport);
        Task UpdateScoutingReport(ScoutingReportRequest scoutingReportRequest, Guid scoutingReportId);
        Task DeleteScoutingReport(Guid scoutingReportId);
        Task<List<ScoutingReport>> RetrieveScoutingReportByScoutId(string scoutId);
    }
}
