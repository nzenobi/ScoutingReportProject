using Heat_Scouting_Report.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ScoutingReportDAL.Repositories;
using ScoutingReportModels;
using ScoutingReportServices.LeagueService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScoutingReportTests.ServiceTests
{
    [TestClass]
    public class LeagueServiceTests
    {
        private readonly Mock<IScoutingReportRepository> _mockScoutingReportRepository = new Mock<IScoutingReportRepository>();
        private readonly Mock<ILogger<LeagueService>> _mockLogger = new Mock<ILogger<LeagueService>>();
        private readonly LeagueService _leagueService;
        private List<League> leagueResponse;

        public LeagueServiceTests()
        {
            _leagueService = new LeagueService(_mockScoutingReportRepository.Object, _mockLogger.Object);
            leagueResponse = new List<League>
            {
                new League { LeagueKey = 1, LeagueName = "League 1" },
                new League { LeagueKey = 2, LeagueName = "League 2" },
            };
        }

        [TestMethod]
        public async Task GetLeagues_ShouldReturnLeagues()
        {
            _mockScoutingReportRepository.Setup(repo => repo.GetLeagues()).ReturnsAsync(leagueResponse);

            var result = await _leagueService.GetLeagues();

            Assert.AreEqual(leagueResponse, result);
        }

        [TestMethod]
        public async Task GetLeagues_ShouldReturnNullWhenExceptionIsThrown()
        {
            var exception = new NullReferenceException();
            _mockScoutingReportRepository.Setup(repo => repo.GetLeagues()).ThrowsAsync(exception);

            var result = await _leagueService.GetLeagues();

            Assert.IsNull(result);
        }
    }
}
