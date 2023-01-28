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
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IScoutingReportRepository> _mockRepository;
        private readonly Mock<ILogger<UserService>> _mockLogger;
        private List<User> expectedScouts;
        public UserServiceTests()
        {
            _mockRepository = new Mock<IScoutingReportRepository>();
            _mockLogger = new Mock<ILogger<UserService>>();
            _userService = new UserService(_mockRepository.Object, _mockLogger.Object);

            expectedScouts = new List<User> 
            {
                new User { AzureAdUserId = "123", CreatedDate = DateTime.Now, Email = "test1@test.com", ModifiedDate = DateTime.Now, Name = "Test Scout 1" },
                new User { AzureAdUserId = "456", CreatedDate = DateTime.Now, Email = "test2@test.com", ModifiedDate = DateTime.Now, Name = "Test Scout 2" }
            };
        }

        [TestMethod]
        public async Task GetActiveScouts_ShouldReturnScouts()
        {
            _mockRepository.Setup(x => x.GetActiveScouts()).ReturnsAsync(expectedScouts);

            var actualScouts = await _userService.GetActiveScouts();

            Assert.AreEqual(expectedScouts.Count, actualScouts.Count);

            for (var i = 0; i < expectedScouts.Count; i++)
            {
                Assert.AreEqual(expectedScouts[i].AzureAdUserId, actualScouts[i].AzureAdUserId);
                Assert.AreEqual(expectedScouts[i].CreatedDate, actualScouts[i].CreatedDate);
                Assert.AreEqual(expectedScouts[i].Email, actualScouts[i].Email);
                Assert.AreEqual(expectedScouts[i].ModifiedDate, actualScouts[i].ModifiedDate);
                Assert.AreEqual(expectedScouts[i].Name, actualScouts[i].Name);
            }

            _mockRepository.Verify(x => x.GetActiveScouts(), Times.Once);
        }

        [TestMethod]
        public async Task GetActiveScouts_ShouldLogErrorIfExceptionIsThrown()
        {
            _mockRepository.Setup(x => x.GetActiveScouts()).ThrowsAsync(new Exception("Test Exception"));

            var actualScouts = await _userService.GetActiveScouts();

            Assert.IsNull(actualScouts);
        }
    }
}
