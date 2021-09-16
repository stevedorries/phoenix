using System.Threading.Tasks;
using Dapper;
using phoenix.Data;
using phoenix.Models;
using phoenix.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace phoenix.Repository
{
    public class ClientRepository: IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<ClientOverview> GetOverviewAsyc() {
             var query = @"SELECT * FROM 
(SELECT COUNT(*) [TotalClients] FROM clients),
(SELECT AVG(credit_score) [AverageCreditScore] FROM credit),
(SELECT COUNT(*) [ClientsMissingCreditData] FROM clients c LEFT JOIN credit cr ON c.id = cr.client_id WHERE cr.credit_score IS NULL),
(SELECT COUNT(id) [ClientsWithApplication] FROM (SELECT DISTINCT c.id FROM clients c JOIN applications a ON c.id = a.client_id));
";
            using (var conn = _context.CreateConnection())
            {
                 var ret = await conn.QuerySingleOrDefaultAsync<ClientOverview>(query);
                 return ret;
            }
        }

        public async Task<IEnumerable<Client>> GetClientsAsync(string search) {
            var query = @"SELECT id, first_name [FirstName], last_name [LastName], email [Email] 
FROM clients
WHERE first_name = @search OR last_name = @search OR email = @search";
            using (var conn = _context.CreateConnection())
            {
                 var ret = await conn.QueryAsync<Client>(query, new {search});
                 return ret.ToList();
            }
        }
    }
}