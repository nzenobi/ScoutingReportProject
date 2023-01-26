using ScoutingReportDAL.Db;
using ScoutingReportModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScoutingReportServices
{
    public interface IScoutingReportService
    {
        public Task<List<ScoutingReportResponse>> GetScoutingReportResponse(string scoutId);
        public Task<ScoutingReport> CreateScoutingReport(ScoutingReportRequest scoutingReportRequest);
        public Task<bool> UpdateScoutingReport(ScoutingReportRequest scoutingReportRequest, Guid scoutingReportId);
        public Task<bool> DeleteScoutingReport(Guid scoutingReportId);
    }
}
