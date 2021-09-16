using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace phoenix.Models
{
    public class Client
    {
        //id,first_name,last_name,email
        [Index(0)]
        public int Id { get; set; }
        [Index(1)]
        public string FirstName { get; set; }
        [Index(2)]
        public string LastName { get; set; }
        [Index(3)]
        public string Email { get; set; }

        [Ignore]
        public List<Application> Applications { get; set; } = new List<Application>();
        [Ignore]
        public Credit CreditScore { get; set; }
    }

    public class ClientOverview {
        public int TotalClients { get; set; }
        public int ClientsWithApplication { get; set; }
        public int ClientsMissingCreditData { get; set; }
        public int AverageCreditScore { get; set; }
    }
}