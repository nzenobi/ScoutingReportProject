using Microsoft.Extensions.Logging;
using ScoutingReportDAL.Db;
using ScoutingReportDAL.Repositories;
using ScoutingReportModels;
using ScoutingReportModels.Users.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutingReportServices
{
    public class UserService : IUserService
    {
        private readonly IScoutingReportRepository _scoutingReportRepository;
        private readonly ILogger<UserService> _logger;
        public UserService(IScoutingReportRepository scoutingReportRepository, ILogger<UserService> logger)
        {
            _scoutingReportRepository = scoutingReportRepository;
            _logger = logger;
        }


        public async Task<List<ScoutResponse>> GetActiveScouts()
        {
            try
            {
                List<User> activeScouts = await _scoutingReportRepository.GetActiveScouts();

                List<ScoutResponse> scouts = new List<ScoutResponse>();

                if(activeScouts != null)
                {
                    foreach(User scout in activeScouts)
                    {
                        ScoutResponse scoutResponse = new ScoutResponse();
                        scoutResponse.AzureAdUserId = scout.AzureAdUserId;
                        scoutResponse.CreatedDate = scout.CreatedDate;
                        scoutResponse.Email = scout.Email;
                        scoutResponse.ModifiedDate = scout.ModifiedDate;
                        scoutResponse.Name = scout.Name;

                        scouts.Add(scoutResponse);
                    }
                }

                return scouts;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error fetching active scouts", ex);
                return null;
            }
        }
        
    }
}
