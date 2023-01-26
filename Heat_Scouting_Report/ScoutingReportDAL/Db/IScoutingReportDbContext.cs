using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ScoutingReportModels;

#nullable disable

namespace ScoutingReportDAL.Db
{
    public interface IScoutingReportDbContext
    {
        public DbSet<League> Leagues { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamPlayer> TeamPlayers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ScoutingReport> ScoutingReports { get; set; }
        Task<int> SaveChanges();
    }
}
