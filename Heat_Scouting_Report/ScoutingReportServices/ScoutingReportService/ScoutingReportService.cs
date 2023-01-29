using Microsoft.Extensions.Logging;
using ScoutingReportDAL.Repositories;
using ScoutingReportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutingReportServices
{
    public class ScoutingReportService : IScoutingReportService
    {
        private readonly IScoutingReportRepository _scoutingReportRepository;
        private readonly ILogger<ScoutingReportService> _logger;
        public ScoutingReportService(ILogger<ScoutingReportService> logger, IScoutingReportRepository scoutingReportRepository)
        {
            _scoutingReportRepository = scoutingReportRepository;
            _logger = logger;
        }

        public async Task<bool> DeleteScoutingReport(Guid scoutingReportId)
        {
            try
            {
                await _scoutingReportRepository.DeleteScoutingReport(scoutingReportId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting scouting report", ex);
                return false;
            }
        }

        public async Task<bool> UpdateScoutingReport(ScoutingReportRequest scoutingReportRequest, Guid scoutingReportId)
        {
            try
            {
                await _scoutingReportRepository.UpdateScoutingReport(scoutingReportRequest, scoutingReportId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating scouting report", ex);
                return false;
            }
        }


        public async Task<ScoutingReportResponse> GetScoutingReportResponse(string scoutId)
        {
            List<ScoutingReport> reports = new List<ScoutingReport>();
            try
            {
                reports = await _scoutingReportRepository.RetrieveScoutingReportByScoutId(scoutId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching scouting reports", ex);
                return null;
            }
            ScoutingReportResponse scoutingReportResponse = new ScoutingReportResponse();
            List<ScoutingReportTeamResponse> scoutingReportTeamResponses = new List<ScoutingReportTeamResponse>();

            // Manually group up scouting reports by team. Must do this to deal with the case where players have multiple 
            // active teams (Player ID: 91492). In this case we are going to show the scouting report under both teams.
            Dictionary<Team, List<ScoutingReport>> groupedScoutingReports = new Dictionary<Team, List<ScoutingReport>>();
            foreach (ScoutingReport report in reports)
            {
                // If a player has no active teams, we will add them to a free agent list of scouting reports
                List<TeamPlayer> activeTeams = report.Player.TeamPlayers.Where(tp => tp.ActiveTeamFlg == true).ToList();
                if (activeTeams != null && activeTeams.Count > 0)
                {
                    foreach (TeamPlayer teamPlayer in activeTeams)
                    {
                        if (groupedScoutingReports.ContainsKey(teamPlayer.TeamKeyNavigation))
                        {
                            if (!groupedScoutingReports[teamPlayer.TeamKeyNavigation].Any(r => r.ScoutingReportId == report.ScoutingReportId))
                            {
                                groupedScoutingReports[teamPlayer.TeamKeyNavigation].Add(report);
                            }
                        }
                        else
                        {
                            groupedScoutingReports[teamPlayer.TeamKeyNavigation] = new List<ScoutingReport>() { report };
                        }
                    }
                }
                else
                {
                    scoutingReportResponse.FreeAgentReports.Add(MapScoutingReportToResponse(report, scoutId));
                }
            }

            foreach (KeyValuePair<Team, List<ScoutingReport>> entry in groupedScoutingReports)
            {
                ScoutingReportTeamResponse scoutingReportTeamResponse = new ScoutingReportTeamResponse();

                Team team = entry.Key;


                scoutingReportTeamResponse.Conference = team.Conference;
                scoutingReportTeamResponse.NickName = team.TeamNickname;
                scoutingReportTeamResponse.TeamId = team.TeamKey;
                scoutingReportTeamResponse.Players = new List<PlayerScoutingReportResponse>();

                Dictionary<Player, List<ScoutingReport>> groupedByPlayer = entry.Value.GroupBy(p => p.Player).ToDictionary(g => g.Key, g => g.ToList());

                foreach (KeyValuePair<Player, List<ScoutingReport>> playerReportEntry in groupedByPlayer)
                {
                    PlayerScoutingReportResponse playerScoutingReport = new PlayerScoutingReportResponse();
                    playerScoutingReport.PlayerId = playerReportEntry.Key.PlayerKey;
                    playerScoutingReport.PlayerName = playerReportEntry.Key.FirstName + " " + playerReportEntry.Key.LastName;
                    playerScoutingReport.DOB = playerReportEntry.Key.BirthDate == null ? "N/A" : playerReportEntry.Key.BirthDate.Value.ToShortDateString();

                    playerScoutingReport.Reports = new List<ScoutingReportsResponse>();
                    foreach(ScoutingReport report in playerReportEntry.Value)
                    {
                        playerScoutingReport.Reports.Add(MapScoutingReportToResponse(report, scoutId));
                    }

                    scoutingReportTeamResponse.Players.Add(playerScoutingReport);
                }
                scoutingReportTeamResponses.Add(scoutingReportTeamResponse);
            }

            scoutingReportResponse.TeamPlayerReports = scoutingReportTeamResponses;

            return scoutingReportResponse;
        }

        private ScoutingReportsResponse MapScoutingReportToResponse(ScoutingReport report, string scoutId)
        {
            ScoutingReportsResponse scoutingReport = new ScoutingReportsResponse();
            scoutingReport.ScoutingReportId = report.ScoutingReportId;
            scoutingReport.Comments = report.Comments;
            scoutingReport.CreatedDateTime = report.CreatedDateTime.ToString("MM/dd/yyyy h:mm tt");
            scoutingReport.ModifiedDateTime = report.ModifiedDateTime != null ? report.ModifiedDateTime.Value.ToString("MM/dd/yyyy h:mm tt") : null;
            scoutingReport.Defense = report.Defense;
            scoutingReport.Rebound = report.Rebound;
            scoutingReport.Shooting = report.Shooting;
            scoutingReport.ScoutId = scoutId;

            return scoutingReport;
        }

        public async Task<ScoutingReport> CreateScoutingReport(ScoutingReportRequest scoutingReportRequest)
        {
            ScoutingReport scoutingReport = new ScoutingReport();
            scoutingReport.ScoutingReportId = new Guid();

            // Set all created date times in EST
            TimeZoneInfo EST = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime convertedDateTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now.DateTime, EST);

            scoutingReport.CreatedDateTime = convertedDateTime;
            scoutingReport.PlayerId = scoutingReportRequest.PlayerId;
            scoutingReport.ScoutId = scoutingReportRequest.ScoutId;

            scoutingReport.Comments = scoutingReportRequest.Comments;
            scoutingReport.Defense = scoutingReportRequest.Defense;
            scoutingReport.Shooting = scoutingReportRequest.Shooting;
            scoutingReport.Rebound = scoutingReportRequest.Rebound;

            scoutingReport.IsActive = true;

            try
            {
                await _scoutingReportRepository.CreateScoutingReport(scoutingReport);
                return scoutingReport;
            } 
            catch(Exception ex)
            {
                _logger.LogError("Error creating scouting report", ex);
                return null;
            }
        }
    }
}
