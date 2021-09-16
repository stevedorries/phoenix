using CsvHelper.Configuration.Attributes;

namespace phoenix.Models
{
    public class Credit
    {
        [Index(0)]
        public int Id { get; set; }
        [Index(1)]
        public int ClientId { get; set; }
        [Index(2)]
        public int CreditScore { get; set; }

        [Ignore]
        public Client Client { get; set; }
    }
}