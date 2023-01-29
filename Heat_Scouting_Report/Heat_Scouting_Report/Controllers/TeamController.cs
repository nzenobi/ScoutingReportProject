using Heat_Scouting_Report.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScoutingReportModels;
using ScoutingReportServices.TeamService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heat_Scouting_Report.Controllers
{
    [ApiController]
    [Route("api/teams")]
    public class TeamController : ControllerBase
    {
        private readonly ILogger<TeamController> _logger;
        private readonly ITeamService _teamService;

        public TeamController(ILogger<TeamController> logger, ITeamService teamService)
        {
            _logger = logger;
            _teamService = teamService;
        }

        [HttpGet]
        [Route("{leagueId}")]
        public async Task<ActionResult> GetTeamsByLeagueId(int leagueId)
        {
            _logger.LogInformation($"Incoming get teams by league Id: {leagueId}");

            List<Team> teams = await _teamService.GetTeamsByLeagueId(leagueId);

            if (teams != null)
            {
                return StatusCode(StatusCodes.Status200OK, teams);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessage() { Message = ErrorMessageConstants.GetLeague });
            }
        }
    }
}
