using Heat_Scouting_Report.RequestValidation;
using Heat_Scouting_Report.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScoutingReportDAL.Db;
using ScoutingReportModels;
using ScoutingReportServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heat_Scouting_Report.Controllers
{
    [ApiController]
    [Route("api/scoutingReport")]
    public class ScoutingReportController : ControllerBase
    {
        private readonly ILogger<ScoutingReportController> _logger;
        private readonly IScoutingReportService _scoutingReportService;

        public ScoutingReportController(ILogger<ScoutingReportController> logger, IScoutingReportService scoutingReportService)
        {
            _logger = logger;
            _scoutingReportService = scoutingReportService;
        }

        [HttpPut]
        [Route("{scoutingReportId}")]
        public async Task<ActionResult> UpdateScoutingReport([FromBody] ScoutingReportRequest scoutingReportRequest, Guid scoutingReportId)
        {
            _logger.LogInformation($"Incoming update scouting report request for {scoutingReportId.ToString()}: {scoutingReportRequest.ToString()}");

            // Validate the ratings in the request are within expected ranges for update
            if (RequestValidationHelper.HasValidUpdatePlayerRatings(scoutingReportRequest))
            {
                bool successfulUpdate = await _scoutingReportService.UpdateScoutingReport(scoutingReportRequest, scoutingReportId);

                if (successfulUpdate)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessage() { Message = ErrorMessageConstants.UpdateScoutingReport });
                }
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("{scoutingReportId}")]
        public async Task<ActionResult> DeleteScoutingReport(Guid scoutingReportId)
        {
            _logger.LogInformation($"Incoming delete scouting report request for: {scoutingReportId.ToString()}");

            bool successfulDelete = await _scoutingReportService.DeleteScoutingReport(scoutingReportId);

            if (successfulDelete)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessage() { Message = ErrorMessageConstants.DeleteScoutingReport });
            }

        }

        [HttpPost]
        public async Task<ActionResult> CreateScoutingReport([FromBody] ScoutingReportRequest scoutingReportRequest)
        {
            _logger.LogInformation($"Incoming create scouting report request: {scoutingReportRequest.ToString()}");

            // Validate the ratings in the request are within expected ranges
            if (RequestValidationHelper.HasValidPlayerRatings(scoutingReportRequest))
            {
                ScoutingReport report = await _scoutingReportService.CreateScoutingReport(scoutingReportRequest);

                if(report != null)
                {
                    return StatusCode(StatusCodes.Status201Created, report);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessage() { Message = ErrorMessageConstants.CreateScoutingReport });
                }
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("{scoutId}")]
        public async Task<ActionResult>  GetScoutingReport(string scoutId)
        {
            _logger.LogInformation($"Incoming get scouting report request for scout: {scoutId}");

            ScoutingReportResponse scoutingReports = await _scoutingReportService.GetScoutingReportResponse(scoutId);

            if(scoutingReports != null)
            {
                return StatusCode(StatusCodes.Status200OK, scoutingReports); ;
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessage() { Message = ErrorMessageConstants.GetScoutingReport });
            }
        }

    }
}
