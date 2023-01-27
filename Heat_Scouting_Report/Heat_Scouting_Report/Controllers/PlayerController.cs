using Heat_Scouting_Report.RequestValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScoutingReportDAL.Db;
using ScoutingReportModels;
using ScoutingReportServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Heat_Scouting_Report.Controllers
{
    [ApiController]
    [Route("api/players")]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;
        private readonly IPlayerService _playerService;

        public PlayerController(ILogger<PlayerController> logger, IPlayerService playerService)
        {
            _logger = logger;
            _playerService = playerService;
        }

        [HttpGet]
        [RouteAttribute("roster")]
        public async Task<ActionResult> GetRoster([FromQuery] RosterRequest rosterRequest)
        {
            _logger.LogInformation($"Incoming get roster request: {rosterRequest.ToString()}");

            List<Player> roster = await _playerService.GetRoster(rosterRequest);

            if(roster != null)
            {
                return StatusCode(StatusCodes.Status200OK, roster);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessage() { Message = "Error fetching roster" });
            }
        }


        [HttpGet]
        public List<Player> QueryPlayers([FromQuery] ActivePlayerRequest activePlayerRequest)
        {
            _logger.LogInformation($"Incoming player search request: {activePlayerRequest.ToString()}");

            var results = _playerService.GetPlayerList(activePlayerRequest);

            return results;
        }

    }
}
