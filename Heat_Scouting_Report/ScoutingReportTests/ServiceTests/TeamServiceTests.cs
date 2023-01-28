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
    class TeamServiceTests
    {
        private readonly TeamService _teamService;
        private readonly Mock<IScoutingReportRepository> _mockScoutingReportRepository;
        private readonly Mock<ILogger<TeamService>> _mockLogger;
        private List<Team> teamsResponse;

        public TeamServiceTests()
        {
            _mockScoutingReportRepository = new Mock<IScoutingReportRepository>();
            _mockLogger = new Mock<ILogger<TeamService>>();
            _teamService = new TeamService(_mockScoutingReportRepository.Object, _mockLogger.Object);

            teamsResponse = new List<Team>()
            {
                new Team() { TeamKey = 1, TeamName = "Test1", TeamNickname = "TESTING"},
                new Team() { TeamKey = 2, TeamName = "Test2", TeamNickname = "MYTEST"},
            };
        }

        [TestMethod]
        public async Task GetTeamsByLeagueId_Should_Return_Teams_When_Successful()
        {
            _mockScoutingReportRepository.Setup(x => x.GetTeams(1)).ReturnsAsync(teamsResponse);

            List<Team> actualTeams = await _teamService.GetTeamsByLeagueId(1);

            Assert.AreEqual(teamsResponse, actualTeams);
            _mockScoutingReportRepository.Verify(x => x.GetTeams(1), Times.Once());
        }

        [TestMethod]
        public async Task GetTeamsByLeagueId_Should_Return_Null_When_Exception_Is_Thrown()
        {
            Exception exception = new Exception();
            _mockScoutingReportRepository.Setup(x => x.GetTeams(1)).ThrowsAsync(exception);

            List<Team> actualTeams = await _teamService.GetTeamsByLeagueId(1);

            Assert.IsNull(actualTeams);
            _mockScoutingReportRepository.Verify(x => x.GetTeams(1), Times.Once());
        }
    }
}
