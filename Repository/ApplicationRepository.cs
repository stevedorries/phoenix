using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using phoenix.Contracts;
using phoenix.Data;
using phoenix.Models;

namespace phoenix.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationOverview> GetOverviewAsync()
        {
            var query = @"SELECT * FROM 
(SELECT COUNT(*) [TotalApplications] FROM applications),
(SELECT COUNT(*) [QualifiedApplications] FROM (
SELECT a.monthly_debt / (a.annual_income / 12) dtir, cr.credit_score 
FROM applications a 
LEFT JOIN clients c ON c.id = a.client_id 
LEFT JOIN credit cr ON cr.client_id = a.client_id
WHERE cr.credit_score IS NOT NULL 
AND a.annual_income IS NOT NULL 
AND a.monthly_debt IS NOT NULL
)
WHERE credit_score > 520 AND dtir < .5
);";
            using (var conn = _context.CreateConnection())
            {
                var ret = await conn.QuerySingleOrDefaultAsync<ApplicationOverview>(query);
                return ret;
            }
        }

        public async Task<IEnumerable<Application>> GetApplicationsAsync(string search) {
            var query = @"SELECT a.id [Id], a.client_id [ClientId], a.annual_income [AnnualIncome], a.monthly_debt [MonthlyDebt] 
FROM applications a 
JOIN clients c ON c.id = a.client_id
WHERE c.first_name = @search OR c.last_name = @search OR c.email = @search";
             using (var conn = _context.CreateConnection())
            {
                var ret = await conn.QueryAsync<Application>(query, new {search});
                return ret.ToList();
            }
        }
    }
}
