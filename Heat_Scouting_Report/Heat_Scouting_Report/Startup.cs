using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ScoutingReportDAL.Db;
using ScoutingReportDAL.Repositories;
using ScoutingReportServices;
using ScoutingReportServices.LeagueService;
using ScoutingReportServices.TeamService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heat_Scouting_Report
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ScoutingReportDbContext>(options =>
                options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"),
                ef => ef.MigrationsAssembly(typeof(ScoutingReportDbContext).Assembly.FullName)));
            services.AddScoped<IScoutingReportDbContext>(provider => provider.GetService<ScoutingReportDbContext>());
            services.AddScoped<IScoutingReportRepository, ScoutingReportRepository>();
            services.AddScoped<IScoutingReportService, ScoutingReportService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILeagueService, LeagueService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            ScoutingReportDbContext db = services.GetService<ScoutingReportDbContext>();
            db.Database.Migrate();
        }
    }
}
