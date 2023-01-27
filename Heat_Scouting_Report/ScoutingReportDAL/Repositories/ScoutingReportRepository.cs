using Microsoft.EntityFrameworkCore;
using ScoutingReportDAL.Db;
using ScoutingReportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutingReportDAL.Repositories
{
    public class ScoutingReportRepository : IScoutingReportRepository
    {
        private readonly ScoutingReportDbContext _dbContext;

        public ScoutingReportRepository(ScoutingReportDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Team>> GetTeams(int leagueId)
        {
            return await _dbContext.Teams.Where(t => t.LeagueKey == leagueId).ToListAsync();
        }

        public async Task<List<League>> GetLeagues()
        {
            return await _dbContext.Leagues.Where(l => l.SearchDisplayFlag == true).ToListAsync();

        }

        public async Task<List<Player>> GetRoster(RosterRequest rosterRequest)
        {
            IQueryable<TeamPlayer> rosterQuery = _dbContext.TeamPlayers
                .Include(tp => tp.PlayerKeyNavigation)
                .Where(tp => tp.SeasonKey == rosterRequest.SeasonId && tp.TeamKey == rosterRequest.TeamId);

            if(rosterRequest.ActiveOnly)
            {
                rosterQuery.Where(tp => tp.ActiveTeamFlg == true);
            }

            List<Player> roster = await rosterQuery.Select(tp => tp.PlayerKeyNavigation).ToListAsync();

            return roster;
        }

        public async Task<List<User>> GetActiveScouts()
        {
            return await _dbContext.Users.Where(u => u.ActiveFlag == true).ToListAsync();
        }

        public List<Player> GetPlayers(ActivePlayerRequest activePlayerRequest)
        {
            var playerQuery = from players in _dbContext.Players
                              join tp in _dbContext.TeamPlayers
                              on players.PlayerKey equals tp.PlayerKey
                              where tp.ActiveTeamFlg == true
                              select players;


            if (!String.IsNullOrEmpty(activePlayerRequest.FirstName))
            {
                playerQuery = playerQuery.Where(p => p.FirstName.Contains(activePlayerRequest.FirstName));
            }
            if (!String.IsNullOrEmpty(activePlayerRequest.LastName))
            {
                playerQuery = playerQuery.Where(p => p.LastName.Contains(activePlayerRequest.LastName));
            }
            if (activePlayerRequest.Season > 0)
            {
                playerQuery = playerQuery.Where(p => p.TeamPlayers.Any(tp => tp.SeasonKey == activePlayerRequest.Season && tp.ActiveTeamFlg == true)); // Lets get confirmation here
            }

            var results = playerQuery.ToList();
            return results;
        }

        public async Task DeleteScoutingReport(Guid scoutingReportId)
        {
            ScoutingReport scoutingReport = _dbContext.ScoutingReports.Where(sr => sr.ScoutingReportId == scoutingReportId).FirstOrDefault();
            if (scoutingReport == null)
            {
                throw new Exception("Cannot find scouting report to update");
            }

            scoutingReport.IsActive = false;

            await _dbContext.SaveChanges();
        }

        public async Task UpdateScoutingReport(ScoutingReportRequest scoutingReportRequest, Guid scoutingReportId)
        {
            ScoutingReport scoutingReport = _dbContext.ScoutingReports.Where(sr => sr.ScoutingReportId == scoutingReportId).FirstOrDefault();
            if (scoutingReport == null)
            {
                throw new Exception("Cannot find scouting report to update");
            }

            if (!String.IsNullOrEmpty(scoutingReportRequest.ScoutId))
            {
                scoutingReport.ScoutId = scoutingReportRequest.ScoutId;
            }
            if (scoutingReportRequest.PlayerId > 0)
            {
                scoutingReport.PlayerId = scoutingReportRequest.PlayerId;
            }
            if (!String.IsNullOrEmpty(scoutingReportRequest.Comments))
            {
                scoutingReport.Comments = scoutingReportRequest.Comments;
            }
            if (scoutingReportRequest.Defense > 0)
            {
                scoutingReport.Defense = scoutingReportRequest.Defense;
            }
            if (scoutingReportRequest.Rebound > 0)
            {
                scoutingReport.Rebound = scoutingReportRequest.Rebound;
            }
            if (scoutingReportRequest.Shooting > 0)
            {
                scoutingReport.Shooting = scoutingReportRequest.Shooting;
            }

            // Set all created date times in EST
            TimeZoneInfo EST = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime convertedDateTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now.DateTime, EST);
            scoutingReport.ModifiedDateTime = convertedDateTime;

            await _dbContext.SaveChanges();
        }

        public async Task CreateScoutingReport(ScoutingReport scoutingReport)
        {
            _dbContext.ScoutingReports.Add(scoutingReport);
            await _dbContext.SaveChanges();
        }

        public async Task<List<ScoutingReport>> RetrieveScoutingReportByScoutId(string scoutId)
        {
            IQueryable<ScoutingReportQueryResult> scoutingReportQuery = from scoutingReport in _dbContext.ScoutingReports
                       join player in _dbContext.Players 
                        on scoutingReport.PlayerId equals player.PlayerKey
                       join tp in _dbContext.TeamPlayers
                        on player.PlayerKey equals tp.PlayerKey
                       join team in _dbContext.Teams
                        on tp.TeamKey equals team.TeamKey
                       where tp.ActiveTeamFlg == true
                       where scoutingReport.IsActive == true
                       where scoutingReport.ScoutId == scoutId
                        select new ScoutingReportQueryResult() { ScoutingReport = scoutingReport, Player = player, TeamPlayer = tp, Team = team};

            List<ScoutingReportQueryResult> scoutingReportQueryResults = await scoutingReportQuery.ToListAsync();

            return scoutingReportQueryResults.Select(r => r.ScoutingReport).ToList();
        }

    }
}
