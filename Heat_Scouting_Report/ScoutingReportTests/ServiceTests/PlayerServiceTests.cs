using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ScoutingReportDAL.Repositories;
using ScoutingReportModels;
using ScoutingReportServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingReportTests.ServiceTests
{
    [TestClass]
    public class PlayerServiceTests
    {
        private readonly PlayerService _playerService;
        private readonly Mock<IScoutingReportRepository> _scoutingReportRepositoryMock;
        private List<Player> playerResponse; 

        public PlayerServiceTests()
        {
            _scoutingReportRepositoryMock = new Mock<IScoutingReportRepository>();
            var loggerMock = new Mock<ILogger<PlayerService>>();
            _playerService = new PlayerService(_scoutingReportRepositoryMock.Object, loggerMock.Object);

            playerResponse = new List<Player>
                {
                    new Player { PlayerKey = 1, FirstName = "Jaden", LastName = "Ivey", TeamPlayers = new List<TeamPlayer>() { new TeamPlayer() { SeasonKey = 2022, TeamKeyNavigation = new Team() { TeamKey = 1} } } },
                    new Player { PlayerKey = 2, FirstName = "Carson", LastName = "Edwards", TeamPlayers = new List<TeamPlayer>() { new TeamPlayer() { SeasonKey = 2022, TeamKeyNavigation = new Team() { TeamKey = 2} } } },
                };
        }

        [TestMethod]
        public async Task GetRoster_ShouldReturnRoster()
        {
            _scoutingReportRepositoryMock
                .Setup(repo => repo.GetRoster(It.IsAny<RosterRequest>()))
                .ReturnsAsync(playerResponse);

            var actualRoster = await _playerService.GetRoster(new RosterRequest());

            Assert.AreEqual(playerResponse, actualRoster);
        }

        [TestMethod]
        public async Task GetRoster_ShouldReturnNullOnException()
        {
            _scoutingReportRepositoryMock
                .Setup(repo => repo.GetRoster(It.IsAny<RosterRequest>()))
                .Throws<Exception>();

            var actualRoster = await _playerService.GetRoster(new RosterRequest());

            Assert.IsNull(actualRoster);
        }
        [TestMethod]
        public async Task GetPlayerList_ShouldReturnActivePlayerList()
        {
            _scoutingReportRepositoryMock
                .Setup(repo => repo.GetPlayers(It.IsAny<ActivePlayerRequest>()))
                .ReturnsAsync(playerResponse);

            var actualActivePlayers = await _playerService.GetPlayerList(new ActivePlayerRequest());

            Assert.AreEqual(2, actualActivePlayers.ActivePlayers.Count);
        }

        [TestMethod]
        public async Task GetPlayerList_ShouldReturnCorrectTeamListWithPlayer()
        {
            _scoutingReportRepositoryMock
                .Setup(repo => repo.GetPlayers(It.IsAny<ActivePlayerRequest>()))
                .ReturnsAsync(playerResponse);

            var actualActivePlayers = await _playerService.GetPlayerList(new ActivePlayerRequest());

            Assert.AreEqual(1, actualActivePlayers.ActivePlayers.Where(p => p.Player.PlayerKey == 1).FirstOrDefault().TeamList[2022].FirstOrDefault().TeamKey);
        }
        
        [TestMethod]
        public async Task GetPlayerList_ShouldReturnNullOnException()
        {
            _scoutingReportRepositoryMock
                .Setup(repo => repo.GetPlayers(It.IsAny<ActivePlayerRequest>()))
                .Throws<Exception>();

            var actualActivePlayers = await _playerService.GetPlayerList(new ActivePlayerRequest());

            Assert.IsNull(actualActivePlayers);
        }
    
    }
}
