using Microsoft.EntityFrameworkCore;
using ScoutingReportDAL.Db;
using ScoutingReportModels;
using ScoutingReportModels.Repository;
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

        public async Task<List<Player>> GetPlayers(ActivePlayerRequest activePlayerRequest)
        {
            IQueryable<ActivePlayerQueryResults> activePlayerQuery = _dbContext.Players.Join(_dbContext.TeamPlayers,
                                        p => p.PlayerKey,
                                        tp => tp.PlayerKey,
                                        (p, tp) => new { p, tp })
                                .Join(_dbContext.Teams,
                                        x => x.tp.TeamKey,
                                        t => t.TeamKey,
                                        (x, t) => new { x.p, x.tp, t })
                                .Select(x => new ActivePlayerQueryResults() { Player = x.p, TeamPlayer = x.tp, Team = x.t });

            if (!String.IsNullOrEmpty(activePlayerRequest.FirstName))
            {
                activePlayerQuery = activePlayerQuery.Where(p => p.Player.FirstName.Contains(activePlayerRequest.FirstName));
            }
            if (!String.IsNullOrEmpty(activePlayerRequest.LastName))
            {
                activePlayerQuery = activePlayerQuery.Where(p => p.Player.LastName.Contains(activePlayerRequest.LastName));
            }
            if (activePlayerRequest.Season > 0)
            {
                activePlayerQuery = activePlayerQuery.Where(p => p.TeamPlayer.SeasonKey == activePlayerRequest.Season);
            }

            List<ActivePlayerQueryResults> results = await activePlayerQuery.ToListAsync();
            return results.Select(r => r.Player).Distinct().ToList();
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
            IQueryable<ScoutingReportQueryResult> scoutingReportQuery = _dbContext.ScoutingReports
                .Distinct()
                .Join(_dbContext.Players,
                scoutingReport => scoutingReport.PlayerId,
                player => player.PlayerKey,
                (scoutingReport, player) => new { ScoutingReport = scoutingReport, Player = player })
                .Join(_dbContext.TeamPlayers,
                x => x.Player.PlayerKey,
                tp => tp.PlayerKey,
                (x, tp) => new { x.ScoutingReport, x.Player, TeamPlayer = tp })
                .Join(_dbContext.Teams,
                x => x.TeamPlayer.TeamKey,
                team => team.TeamKey,
                (x, team) => new { x.ScoutingReport, x.Player, x.TeamPlayer, Team = team })
                .Where(x => x.ScoutingReport.IsActive == true)
                .Where(x => x.ScoutingReport.ScoutId == scoutId)
                .Select(x => new ScoutingReportQueryResult() { ScoutingReport = x.ScoutingReport, Player = x.Player, TeamPlayer = x.TeamPlayer, Team = x.Team });

            List<ScoutingReportQueryResult> scoutingReportQueryResults = await scoutingReportQuery.ToListAsync();

            Dictionary<Guid, List<ScoutingReport>> scoutingReportList = scoutingReportQueryResults.Select(r => r.ScoutingReport).GroupBy(r => r.ScoutingReportId).ToDictionary(sr => sr.Key, sr => sr.ToList());

            return scoutingReportList.Select(x => x.Value.First()).ToList();
        }

    }
}
