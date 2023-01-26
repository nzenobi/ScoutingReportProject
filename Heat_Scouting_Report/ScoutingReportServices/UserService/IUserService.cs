using ScoutingReportDAL.Db;
using ScoutingReportModels;
using ScoutingReportModels.Users.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScoutingReportServices
{
    public interface IUserService
    {
        Task<List<ScoutResponse>> GetActiveScouts();
    }
}
