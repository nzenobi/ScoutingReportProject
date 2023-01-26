using ScoutingReportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heat_Scouting_Report.RequestValidation
{
    public class RequestValidationHelper
    {
        internal static bool HasValidPlayerRatings(ScoutingReportRequest scoutingReportRequest)
        {
            return scoutingReportRequest != null && (scoutingReportRequest.Defense >= 1 && scoutingReportRequest.Defense <= 10)
                && (scoutingReportRequest.Shooting >= 1 && scoutingReportRequest.Shooting <= 10)
                && (scoutingReportRequest.Rebound >= 1 && scoutingReportRequest.Rebound <= 10);
        }

        internal static bool HasValidUpdatePlayerRatings(ScoutingReportRequest scoutingReportRequest)
        {
            return scoutingReportRequest != null && (scoutingReportRequest.Defense >= 0 && scoutingReportRequest.Defense <= 10)
                && (scoutingReportRequest.Shooting >= 0 && scoutingReportRequest.Shooting <= 10)
                && (scoutingReportRequest.Rebound >= 0 && scoutingReportRequest.Rebound <= 10);
        }
    }
}
