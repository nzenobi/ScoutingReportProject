using Heat_Scouting_Report.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ScoutingReportModels;
using ScoutingReportServices;
using ScoutingReportServices.LeagueService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScoutingReportTests.ControllerTests
{
    [TestClass]
    public class PlayerControllerTest
    {
        private Mock<ILogger<PlayerController>> mockLogger;
        private PlayerController playerController;
        private Mock<IPlayerService> mockPlayerService;
        private RosterRequest rosterRequest;
        private ActivePlayerRequest activePlayerRequest;

        [TestInitialize]
        public void Initialize()
        {
            mockPlayerService = new Mock<IPlayerService>();
            mockLogger = new Mock<ILogger<PlayerController>>();
            playerController = new PlayerController(mockLogger.Object, mockPlayerService.Object);
            rosterRequest = new RosterRequest();
            activePlayerRequest = new ActivePlayerRequest();
        }

        [TestMethod]
        public async Task GetRoster_ReturnsSuccessOnNonNullResponse()
        {
            mockPlayerService.Setup(x => x.GetRoster(rosterRequest)).Returns(Task.FromResult(new List<Player>()));
            var responseFromController = (ObjectResult)await playerController.GetRoster(rosterRequest);
            Assert.AreEqual(200, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task GetRoster_Returns500OnNullResponse()
        {
            mockPlayerService.Setup(x => x.GetRoster(rosterRequest)).Returns(Task.FromResult((List<Player>)null));
            var responseFromController = (ObjectResult)await playerController.GetRoster(rosterRequest);
            Assert.AreEqual(500, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task GetRoster_ReturnsErrorMessageOnNullResponse()
        {
            mockPlayerService.Setup(x => x.GetRoster(rosterRequest)).Returns(Task.FromResult((List<Player>)null));
            var responseFromController = (ObjectResult)await playerController.GetRoster(rosterRequest);

            var errorMessage = (ErrorMessage)responseFromController.Value;
            Assert.AreEqual("Error fetching roster", errorMessage.Message); // TODO Make constant
        }


        [TestMethod]
        public async Task QueryPlayers_ReturnsSuccessOnNonNullResponse()
        {
            mockPlayerService.Setup(x => x.GetPlayerList(activePlayerRequest)).Returns(Task.FromResult(new ActivePlayerResponse()));
            var responseFromController = (ObjectResult)await playerController.QueryPlayers(activePlayerRequest);
            Assert.AreEqual(200, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task QueryPlayers_Returns500OnNullResponse()
        {
            mockPlayerService.Setup(x => x.GetPlayerList(activePlayerRequest)).Returns(Task.FromResult((ActivePlayerResponse)null));
            var responseFromController = (ObjectResult)await playerController.QueryPlayers(activePlayerRequest);
            Assert.AreEqual(500, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task QueryPlayers_ReturnsErrorMessageOnNullResponse()
        {
            mockPlayerService.Setup(x => x.GetPlayerList(activePlayerRequest)).Returns(Task.FromResult((ActivePlayerResponse)null));
            var responseFromController = (ObjectResult)await playerController.QueryPlayers(activePlayerRequest);

            var errorMessage = (ErrorMessage)responseFromController.Value;
            Assert.AreEqual("Error fetching active players", errorMessage.Message); // TODO Make constant
        }
    }
}
