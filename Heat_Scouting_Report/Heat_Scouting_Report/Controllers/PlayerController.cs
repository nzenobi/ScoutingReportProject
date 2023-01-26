using Heat_Scouting_Report.RequestValidation;
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
        public List<Player> QueryPlayers([FromQuery] ActivePlayerRequest activePlayerRequest)
        {
            _logger.LogInformation($"Incoming player search request: {activePlayerRequest.ToString()}");

            var results = _playerService.GetPlayerList(activePlayerRequest);

            return results;
        }

    }
}
