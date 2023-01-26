using System;
using System.Collections.Generic;
using System.Text;

namespace ScoutingReportModels.Users.Response
{
    public class ScoutResponse
    {
        public string AzureAdUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
