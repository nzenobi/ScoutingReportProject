using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ScoutingReportDAL.Repositories;
using ScoutingReportModels;
using ScoutingReportServices;
using ScoutingReportServices.TeamService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingReportTests.ServiceTests
{
    [TestClass]
    public class ScoutingReportServiceTests
    {
        private readonly Mock<IScoutingReportRepository> _scoutingReportRepositoryMock;
        private readonly Mock<ILogger<ScoutingReportService>> _loggerMock;
        private readonly ScoutingReportService _scoutingReportService;
        private List<ScoutingReport> scoutingReportRepoResponse;
        public ScoutingReportServiceTests()
        {
            _scoutingReportRepositoryMock = new Mock<IScoutingReportRepository>();
            _loggerMock = new Mock<ILogger<ScoutingReportService>>();
            _scoutingReportService = new ScoutingReportService(_loggerMock.Object, _scoutingReportRepositoryMock.Object);

            scoutingReportRepoResponse = new List<ScoutingReport>
            {
                new ScoutingReport
                {
                    ScoutingReportId = new Guid(),
                    Comments = "Test report",
                    CreatedDateTime = DateTime.Now,
                    Defense = 5,
                    Rebound = 4,
                    Shooting = 8,
                    Player = new Player
                    {
                        PlayerKey = 100,
                        FirstName = "Carson",
                        LastName = "Edwards",
                        BirthDate = new DateTime(1996, 1, 1),
                        TeamPlayers = new List<TeamPlayer>
                        {
                            new TeamPlayer
                            {
                                TeamKeyNavigation = new Team
                                {
                                    TeamKey = 2,
                                    Conference = "West",
                                    TeamNickname = "Test team"
                                }
                            }
                        }
                    }
                }
            };
        }

        [TestMethod]
        public async Task DeleteScoutingReport_CallsDeleteScoutingReportOnRepository()
        {
            Guid scoutingReportId = Guid.NewGuid();

            await _scoutingReportService.DeleteScoutingReport(scoutingReportId);

            _scoutingReportRepositoryMock.Verify(x => x.DeleteScoutingReport(scoutingReportId), Times.Once);
        }

        [TestMethod]
        public async Task DeleteScoutingReport_ReturnsTrue_OnSuccess()
        {
            var result = await _scoutingReportService.DeleteScoutingReport(Guid.NewGuid());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task DeleteScoutingReport_ReturnsFalse_OnException()
        {
            _scoutingReportRepositoryMock.Setup(x => x.DeleteScoutingReport(It.IsAny<Guid>())).Throws(new Exception());

            var result = await _scoutingReportService.DeleteScoutingReport(Guid.NewGuid());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task UpdateScoutingReport_CallsUpdateScoutingReportOnRepository()
        {
            var scoutingReportRequest = new ScoutingReportRequest();
            Guid scoutingReportId = Guid.NewGuid();

            await _scoutingReportService.UpdateScoutingReport(scoutingReportRequest, scoutingReportId);

            _scoutingReportRepositoryMock.Verify(x => x.UpdateScoutingReport(scoutingReportRequest, scoutingReportId), Times.Once);
        }

        [TestMethod]
        public async Task UpdateScoutingReport_ReturnsTrue_OnSuccess()
        {
            var result = await _scoutingReportService.UpdateScoutingReport(new ScoutingReportRequest(), Guid.NewGuid());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UpdateScoutingReport_ReturnsFals_OnException()
        {
            _scoutingReportRepositoryMock.Setup(x => x.UpdateScoutingReport(It.IsAny<ScoutingReportRequest>(), It.IsAny<Guid>())).Throws(new Exception());

            var result = await _scoutingReportService.UpdateScoutingReport(new ScoutingReportRequest(), Guid.NewGuid());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task CreateScoutingReport_ShouldReturnNull_OnException()
        {
            _scoutingReportRepositoryMock.Setup(x => x.CreateScoutingReport(It.IsAny<ScoutingReport>())).Throws(new Exception());

            var scoutingReportRequest = new ScoutingReportRequest
            {
                PlayerId = 1,
                ScoutId = "10",
                Comments = "Test comments",
                Defense = 3,
                Shooting = 4,
                Rebound = 5
            };

            var expectedScoutingReport = new ScoutingReport
            {
                ScoutingReportId = Guid.NewGuid(),
                PlayerId = 1,
                ScoutId = "10",
                Comments = "Test comments",
                Defense = 3,
                Shooting = 4,
                Rebound = 5,
                IsActive = true,
                CreatedDateTime = DateTimeOffset.Now.DateTime
            };

            var result = await _scoutingReportService.CreateScoutingReport(scoutingReportRequest);

            Assert.IsNull(result);
           
            _scoutingReportRepositoryMock.Verify(x => x.CreateScoutingReport(It.IsAny<ScoutingReport>()), Times.Once);
        }

        [TestMethod]
        public async Task CreateScoutingReport_ShouldReturnScoutingReport_OnSuccess()
        {
            var scoutingReportRequest = new ScoutingReportRequest
            {
                PlayerId = 1,
                ScoutId = "10",
                Comments = "Test comments",
                Defense = 3,
                Shooting = 4,
                Rebound = 5
            };

            var expectedScoutingReport = new ScoutingReport
            {
                ScoutingReportId = Guid.NewGuid(),
                PlayerId = 1,
                ScoutId = "10",
                Comments = "Test comments",
                Defense = 3,
                Shooting = 4,
                Rebound = 5,
                IsActive = true,
                CreatedDateTime = DateTimeOffset.Now.DateTime
            };

            _scoutingReportRepositoryMock
                .Setup(x => x.CreateScoutingReport(It.IsAny<ScoutingReport>()))
                .Returns(Task.CompletedTask);

            var result = await _scoutingReportService.CreateScoutingReport(scoutingReportRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedScoutingReport.PlayerId, result.PlayerId);
            Assert.AreEqual(expectedScoutingReport.ScoutId, result.ScoutId);
            Assert.AreEqual(expectedScoutingReport.Comments, result.Comments);
            Assert.AreEqual(expectedScoutingReport.Defense, result.Defense);
            Assert.AreEqual(expectedScoutingReport.Shooting, result.Shooting);
            Assert.AreEqual(expectedScoutingReport.Rebound, result.Rebound);
            Assert.AreEqual(expectedScoutingReport.IsActive, result.IsActive);
            _scoutingReportRepositoryMock.Verify(x => x.CreateScoutingReport(It.IsAny<ScoutingReport>()), Times.Once);
        }

        [TestMethod]
        public async Task GetScoutingReportResponse_ShouldReturnScoutingReportResponses()
        {
            string scoutId = "123";

            _scoutingReportRepositoryMock.Setup(x => x.RetrieveScoutingReportByScoutId(scoutId)).ReturnsAsync(scoutingReportRepoResponse);

            var result = await _scoutingReportService.GetScoutingReportResponse(scoutId);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            // Assert Team
            var firstReportResponse = result.First();
            Assert.AreEqual("West", firstReportResponse.conference);
            Assert.AreEqual("Test team", firstReportResponse.nickName);
            Assert.AreEqual(2, firstReportResponse.teamId);

            // Assert Player
            var player = result.FirstOrDefault().players.FirstOrDefault();
            Assert.AreEqual(100, player.playerId);
            Assert.AreEqual("Carson Edwards", player.playerName);
            Assert.AreEqual("1/1/1996", player.dob);

            // Assert Report
            var report = result.FirstOrDefault().players.FirstOrDefault().reports.FirstOrDefault();
            Assert.AreEqual("Test report", report.comments);
            Assert.AreEqual(8, report.shooting);
            Assert.AreEqual(4, report.rebound);
            Assert.AreEqual(5, report.defense);
        }

        [TestMethod]
        public async Task GetScoutingReportResponse_ShouldReturnNullOnException()
        {
            string scoutId = "123";

            _scoutingReportRepositoryMock.Setup(x => x.RetrieveScoutingReportByScoutId(scoutId)).ThrowsAsync(new Exception());

            var result = await _scoutingReportService.GetScoutingReportResponse(scoutId);

            Assert.IsNull(result);
        }

    }
    }
