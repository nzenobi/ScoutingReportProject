using Heat_Scouting_Report.Controllers;
using Heat_Scouting_Report.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ScoutingReportModels;
using ScoutingReportModels.Users.Response;
using ScoutingReportServices;
using ScoutingReportServices.LeagueService;
using ScoutingReportServices.TeamService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScoutingReportTests.ControllerTests
{
    [TestClass]
    public class UserControllerTest
    {
        private Mock<ILogger<UserController>> mockLogger;
        private UserController userController;
        private Mock<IUserService> mockUserService;

        [TestInitialize]
        public void Initialize()
        {
            mockUserService = new Mock<IUserService>();
            mockLogger = new Mock<ILogger<UserController>>();
            userController = new UserController(mockLogger.Object, mockUserService.Object);
        }

        [TestMethod]
        public async Task GetActiveScouts_ReturnsSuccessOnNonNullResponse()
        {
            mockUserService.Setup(x => x.GetActiveScouts()).Returns(Task.FromResult(new List<ScoutResponse>()));
            var responseFromController = (ObjectResult)await userController.GetActiveScouts();
            Assert.AreEqual(200, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task GetActiveScouts_Returns500OnNullResponse()
        {
            mockUserService.Setup(x => x.GetActiveScouts()).Returns(Task.FromResult((List<ScoutResponse>)null));
            var responseFromController = (ObjectResult)await userController.GetActiveScouts();
            Assert.AreEqual(500, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task GetActiveScouts_ReturnsErrorMessageOnNullResponse()
        {
            mockUserService.Setup(x => x.GetActiveScouts()).Returns(Task.FromResult((List<ScoutResponse>)null));
            var responseFromController = (ObjectResult)await userController.GetActiveScouts();

            var errorMessage = (ErrorMessage)responseFromController.Value;
            Assert.AreEqual(ErrorMessageConstants.ActiveScouts, errorMessage.Message); // TODO Make constant
        }
    }
}
