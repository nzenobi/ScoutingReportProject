using Heat_Scouting_Report.Controllers;
using Heat_Scouting_Report.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ScoutingReportModels;
using ScoutingReportServices;
using ScoutingReportServices.LeagueService;
using ScoutingReportServices.TeamService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScoutingReportTests.ControllerTests
{
    [TestClass]
    public class TeamControllerTest
    {
        private Mock<ILogger<TeamController>> mockLogger;
        private TeamController teamController;
        private Mock<ITeamService> mockTeamService;

        [TestInitialize]
        public void Initialize()
        {
            mockTeamService = new Mock<ITeamService>();
            mockLogger = new Mock<ILogger<TeamController>>();
            teamController = new TeamController(mockLogger.Object, mockTeamService.Object);
        }

        [TestMethod]
        public async Task GetTeamsByLeagueId_ReturnsSuccessOnNonNullResponse()
        {
            mockTeamService.Setup(x => x.GetTeamsByLeagueId(1)).Returns(Task.FromResult(new List<Team>()));
            var responseFromController = (ObjectResult)await teamController.GetTeamsByLeagueId(1);
            Assert.AreEqual(200, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task GetTeamsByLeagueId_Returns500OnNullResponse()
        {
            mockTeamService.Setup(x => x.GetTeamsByLeagueId(1)).Returns(Task.FromResult((List<Team>)null));
            var responseFromController = (ObjectResult)await teamController.GetTeamsByLeagueId(1);
            Assert.AreEqual(500, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task GetTeamsByLeagueId_ReturnsErrorMessageOnNullResponse()
        {
            mockTeamService.Setup(x => x.GetTeamsByLeagueId(1)).Returns(Task.FromResult((List<Team>)null));
            var responseFromController = (ObjectResult)await teamController.GetTeamsByLeagueId(1);

            var errorMessage = (ErrorMessage)responseFromController.Value;
            Assert.AreEqual(ErrorMessageConstants.GetLeague, errorMessage.Message); // TODO Make constant
        }
    }
}
