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


        public async Task<List<ScoutingReportResponse>> GetScoutingReportResponse(string scoutId)
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

            List<ScoutingReportResponse> scoutingReportResponses = new List<ScoutingReportResponse>();

            // Manually group up scouting reports by team. Must do this to deal with the case where players have multiple 
            // active teams (Player ID: 91492). In this case we are going to show the scouting report under both teams.
            Dictionary<Team, List<ScoutingReport>> groupedScoutingReports = new Dictionary<Team, List<ScoutingReport>>();
            foreach (ScoutingReport report in reports)
            {
                foreach(TeamPlayer teamPlayer in report.Player.TeamPlayers)
                {
                    if(groupedScoutingReports.ContainsKey(teamPlayer.TeamKeyNavigation))
                    {
                        if(!groupedScoutingReports[teamPlayer.TeamKeyNavigation].Any(r => r.ScoutingReportId == report.ScoutingReportId))
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

            //Dictionary<int, List<ScoutingReport>> groupedScoutingReports = reports.GroupBy(reports => reports.Player.TeamPlayers.FirstOrDefault().TeamKey).ToDictionary(g => g.Key, g => g.ToList());

            foreach (KeyValuePair<Team, List<ScoutingReport>> entry in groupedScoutingReports)
            {
                ScoutingReportResponse scoutingReportResponse = new ScoutingReportResponse();

                Team team = entry.Key;


                scoutingReportResponse.conference = team.Conference;
                scoutingReportResponse.nickName = team.TeamNickname;
                scoutingReportResponse.teamId = team.TeamKey;
                scoutingReportResponse.players = new List<PlayerScoutingReportResponse>();

                Dictionary<Player, List<ScoutingReport>> groupedByPlayer = entry.Value.GroupBy(p => p.Player).ToDictionary(g => g.Key, g => g.ToList());

                foreach (KeyValuePair<Player, List<ScoutingReport>> playerReportEntry in groupedByPlayer)
                {
                    PlayerScoutingReportResponse playerScoutingReport = new PlayerScoutingReportResponse();
                    playerScoutingReport.playerId = playerReportEntry.Key.PlayerKey;
                    playerScoutingReport.playerName = playerReportEntry.Key.FirstName + " " + playerReportEntry.Key.LastName;
                    playerScoutingReport.dob = playerReportEntry.Key.BirthDate == null ? "N/A" : playerReportEntry.Key.BirthDate.Value.ToShortDateString();

                    playerScoutingReport.reports = new List<ScoutingReportsResponse>();
                    foreach(ScoutingReport report in playerReportEntry.Value)
                    {
                        ScoutingReportsResponse scoutingReport = new ScoutingReportsResponse();
                        scoutingReport.ScoutingReportId = report.ScoutingReportId;
                        scoutingReport.comments = report.Comments;
                        scoutingReport.createdDateTime = report.CreatedDateTime.ToString("MM/dd/yyyy h:mm tt");
                        scoutingReport.ModifiedDateTime = report.ModifiedDateTime != null ? report.ModifiedDateTime.Value.ToString("MM/dd/yyyy h:mm tt") : null;
                        scoutingReport.defense = report.Defense;
                        scoutingReport.rebound = report.Rebound;
                        scoutingReport.shooting = report.Shooting;
                        scoutingReport.scoutId = scoutId;
                        playerScoutingReport.reports.Add(scoutingReport);
                    }

                    scoutingReportResponse.players.Add(playerScoutingReport);
                }
                scoutingReportResponses.Add(scoutingReportResponse);
            }

            return scoutingReportResponses;
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
