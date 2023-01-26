using Heat_Scouting_Report.RequestValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScoutingReportDAL.Db;
using ScoutingReportModels;
using ScoutingReportModels.Users.Response;
using ScoutingReportServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Heat_Scouting_Report.Controllers
{
    [ApiController]
    [Route("api/scouts")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetActiveScouts()
        {
            _logger.LogInformation($"Incoming get active scouts request");

            List<ScoutResponse> activeScouts = await _userService.GetActiveScouts();

            if(activeScouts != null)
            {
                return StatusCode(StatusCodes.Status200OK, activeScouts);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessage() { Message = "Error fetching active scouts" });
            }
        }

    }
}
