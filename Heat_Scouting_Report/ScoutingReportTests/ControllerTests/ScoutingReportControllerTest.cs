using Heat_Scouting_Report.Controllers;
using Heat_Scouting_Report.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ScoutingReportModels;
using ScoutingReportServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingReportTests.ControllerTests
{
    [TestClass]
    public class ScoutingReportControllerTest
    {
        private Mock<ILogger<ScoutingReportController>> mockLogger;
        private ScoutingReportController scoutingReportController;
        private Mock<IScoutingReportService> mockScoutingReportService;
        private ScoutingReportRequest scoutingReportRequest;

        [TestInitialize]
        public void Initialize()
        {
            mockScoutingReportService = new Mock<IScoutingReportService>();
            mockLogger = new Mock<ILogger<ScoutingReportController>>();
            scoutingReportController = new ScoutingReportController(mockLogger.Object, mockScoutingReportService.Object);
            scoutingReportRequest = new ScoutingReportRequest() { Comments = "Test", Defense = 2, Rebound = 3, Shooting = 8, PlayerId = 100, ScoutId = "10" };
        }

        #region Update SR
        [TestMethod]
        public async Task UpdateScoutingReport_ReturnsSuccessOnTrueResponse()
        {
            mockScoutingReportService.Setup(x => x.UpdateScoutingReport(scoutingReportRequest, new Guid())).Returns(Task.FromResult(true));
            var responseFromController = (StatusCodeResult)await scoutingReportController.UpdateScoutingReport(scoutingReportRequest, new Guid());
            Assert.AreEqual(204, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task UpdateScoutingReport_Returns500OnfalseResponse()
        {
            mockScoutingReportService.Setup(x => x.UpdateScoutingReport(scoutingReportRequest, new Guid())).Returns(Task.FromResult(false));
            var responseFromController = (ObjectResult)await scoutingReportController.UpdateScoutingReport(scoutingReportRequest, new Guid());
            Assert.AreEqual(500, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task UpdateScoutingReport_ReturnsErrorMessageOn500Response()
        {
            mockScoutingReportService.Setup(x => x.UpdateScoutingReport(scoutingReportRequest, new Guid())).Returns(Task.FromResult(false));
            var responseFromController = (ObjectResult)await scoutingReportController.UpdateScoutingReport(scoutingReportRequest, new Guid());

            var errorMessage = (ErrorMessage)responseFromController.Value;
            Assert.AreEqual(ErrorMessageConstants.UpdateScoutingReport, errorMessage.Message); // TODO Make constant
        }

        [TestMethod]
        public async Task UpdateScoutingReport_ReturnsBadRequestForInvalidPlayerRatings()
        {
            scoutingReportRequest = new ScoutingReportRequest() { Comments = "Test", Defense = 2, Rebound = 30, Shooting = 8, PlayerId = 100, ScoutId = "10" };
            mockScoutingReportService.Setup(x => x.UpdateScoutingReport(scoutingReportRequest, new Guid())).Returns(Task.FromResult(true));
            var responseFromController = (StatusCodeResult)await scoutingReportController.UpdateScoutingReport(scoutingReportRequest, new Guid());
            Assert.AreEqual(400, responseFromController.StatusCode);
        }
        #endregion

        #region Delete SR
        [TestMethod]
        public async Task DeleteScoutingReport_ReturnsSuccessOnTrueResponse()
        {
            mockScoutingReportService.Setup(x => x.DeleteScoutingReport(new Guid())).Returns(Task.FromResult(true));
            var responseFromController = (StatusCodeResult)await scoutingReportController.DeleteScoutingReport(new Guid());
            Assert.AreEqual(204, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task DeleteScoutingReport_Returns500OnfalseResponse()
        {
            mockScoutingReportService.Setup(x => x.DeleteScoutingReport(new Guid())).Returns(Task.FromResult(false));
            var responseFromController = (ObjectResult)await scoutingReportController.DeleteScoutingReport(new Guid());
            Assert.AreEqual(500, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task DeleteScoutingReport_ReturnsErrorMessageOn500Response()
        {
            mockScoutingReportService.Setup(x => x.DeleteScoutingReport(new Guid())).Returns(Task.FromResult(false));
            var responseFromController = (ObjectResult)await scoutingReportController.DeleteScoutingReport(new Guid());

            var errorMessage = (ErrorMessage)responseFromController.Value;
            Assert.AreEqual(ErrorMessageConstants.DeleteScoutingReport, errorMessage.Message); // TODO Make constant
        }
        #endregion

        #region Create SR
        [TestMethod]
        public async Task CreateScoutingReport_ReturnsSuccessOnNonNullResponse()
        {
            mockScoutingReportService.Setup(x => x.CreateScoutingReport(scoutingReportRequest)).Returns(Task.FromResult(new ScoutingReport()));
            var responseFromController = (ObjectResult)await scoutingReportController.CreateScoutingReport(scoutingReportRequest);
            Assert.AreEqual(201, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task CreateScoutingReport_Returns500OnNullResponse()
        {
            mockScoutingReportService.Setup(x => x.CreateScoutingReport(scoutingReportRequest)).Returns(Task.FromResult((ScoutingReport) null));
            var responseFromController = (ObjectResult)await scoutingReportController.CreateScoutingReport(scoutingReportRequest);
            Assert.AreEqual(500, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task CreateScoutingReport_ReturnsErrorMessageOn500Response()
        {
            mockScoutingReportService.Setup(x => x.CreateScoutingReport(scoutingReportRequest)).Returns(Task.FromResult((ScoutingReport)null));
            var responseFromController = (ObjectResult)await scoutingReportController.CreateScoutingReport(scoutingReportRequest);

            var errorMessage = (ErrorMessage)responseFromController.Value;
            Assert.AreEqual(ErrorMessageConstants.CreateScoutingReport, errorMessage.Message); // TODO Make constant
        }

        [TestMethod]
        public async Task CreateScoutingReport_ReturnsBadRequestForInvalidPlayerRatings()
        {
            scoutingReportRequest = new ScoutingReportRequest() { Comments = "Test", Defense = 2, Rebound = 30, Shooting = 8, PlayerId = 100, ScoutId = "10" };
            mockScoutingReportService.Setup(x => x.CreateScoutingReport(scoutingReportRequest)).Returns(Task.FromResult(new ScoutingReport()));
            var responseFromController = (StatusCodeResult)await scoutingReportController.CreateScoutingReport(scoutingReportRequest);
            Assert.AreEqual(400, responseFromController.StatusCode);
        }
        #endregion

        #region Get SR
        [TestMethod]
        public async Task GetScoutingReport_ReturnsSuccessOnNonNullResponse()
        {
            mockScoutingReportService.Setup(x => x.GetScoutingReportResponse("10")).Returns(Task.FromResult(new ScoutingReportResponse()));
            var responseFromController = (ObjectResult)await scoutingReportController.GetScoutingReport("10");
            Assert.AreEqual(200, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task GetScoutingReport_Returns500OnNullResponse()
        {
            mockScoutingReportService.Setup(x => x.GetScoutingReportResponse("10")).Returns(Task.FromResult((ScoutingReportResponse) null));
            var responseFromController = (ObjectResult)await scoutingReportController.GetScoutingReport("10");
            Assert.AreEqual(500, responseFromController.StatusCode);
        }

        [TestMethod]
        public async Task GetScoutingReport_ReturnsErrorMessageOn500Response()
        {
            mockScoutingReportService.Setup(x => x.GetScoutingReportResponse("10")).Returns(Task.FromResult((ScoutingReportResponse)null));
            var responseFromController = (ObjectResult)await scoutingReportController.GetScoutingReport("10");

            var errorMessage = (ErrorMessage)responseFromController.Value;
            Assert.AreEqual(ErrorMessageConstants.GetScoutingReport, errorMessage.Message); // TODO Make constant
        }
        #endregion
    }
}
