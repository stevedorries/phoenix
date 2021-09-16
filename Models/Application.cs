using CsvHelper.Configuration.Attributes;

namespace phoenix.Models
{
    public class Application
    {
        //id,clientId,annualIncome,monthlyDebt
        [Index(0)]
        public int Id { get; set; }
        [Index(1)]
        public int ClientId { get; set; }
        [Index(2)]
        public double AnnualIncome { get; set; }
        [Index(3)]
        public double MonthlyDebt { get; set; }
    }

    public class ApplicationOverview
    {
        public int TotalApplications { get; set; }

        /// <summary>At the time of writing(9/14/2021) an application is considered qualified IF
        /// The debt:income ratio < .5, debt:income being defined as monthly debt divided by monthly income, if either piece of information is missing that disqualifies the application
        /// AND
        /// The credit score is above 520, a missing credit score is disqualifying
        /// </summary>
        public int QualifiedApplications { get; set; }

        
    }
}