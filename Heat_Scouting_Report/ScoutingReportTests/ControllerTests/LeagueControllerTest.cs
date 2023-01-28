using Heat_Scouting_Report.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ScoutingReportModels;
using ScoutingReportServices.LeagueService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScoutingReportTests.ControllerTests
{
    [TestClass]
    public class LeagueControllerTest
    {
        private Mock<ILogger<LeagueController>> mockLogger;
        private LeagueController leagueController;
        private Mock<ILeagueService> mockLeagueService;

        [TestInitialize]
        public void Initialize()
        {
            mockLeagueService = new Mock<ILeagueService>();
            mockLogger = new Mock<ILogger<LeagueController>>();
            leagueController = new LeagueController(mockLogger.Object, mockLeagueService.Object);
        }

        [TestMethod]
        public async Task GetActiveLeagues_ReturnsSuccessOnNonNullResponse()
        {
            mockLeagueService.Setup(x => x.GetLeagues()).Returns(Task.FromResult(new List<League>()));
            var responseFromController = (ObjectResult)await leagueController.GetActiveLeagues();
            Assert.AreEqual(200, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task GetActiveLeagues_Returns500OnNullResponse()
        {
            mockLeagueService.Setup(x => x.GetLeagues()).Returns(Task.FromResult((List<League>)null));
            var responseFromController = (ObjectResult)await leagueController.GetActiveLeagues();
            Assert.AreEqual(500, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task GetActiveLeagues_ReturnsErrorMessageOnNullResponse()
        {
            mockLeagueService.Setup(x => x.GetLeagues()).Returns(Task.FromResult((List<League>)null));
            var responseFromController = (ObjectResult)await leagueController.GetActiveLeagues();

            var errorMessage = (ErrorMessage)responseFromController.Value;
            Assert.AreEqual("Error getting active leagues", errorMessage.Message); // TODO Make constant
        }
    }

}
