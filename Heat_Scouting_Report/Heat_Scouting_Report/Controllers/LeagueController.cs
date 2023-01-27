using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScoutingReportModels;
using ScoutingReportServices.LeagueService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heat_Scouting_Report.Controllers
{

    [ApiController]
    [Route("api/leagues")]
    public class LeagueController : ControllerBase
    {
        private readonly ILogger<LeagueController> _logger;
        private readonly ILeagueService _leagueService;

        public LeagueController(ILogger<LeagueController> logger, ILeagueService leagueService)
        {
            _logger = logger;
            _leagueService = leagueService;
        }


        [HttpGet]
        public async Task<ActionResult> GetActiveLeagues()
        {
            _logger.LogInformation($"Incoming get active leagues request");

            List<League> leagues = await _leagueService.GetLeagues();

            if(leagues != null)
            {
                return StatusCode(StatusCodes.Status200OK, leagues);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessage() { Message = "Error getting active leagues" });
            }
        }
    }
}
