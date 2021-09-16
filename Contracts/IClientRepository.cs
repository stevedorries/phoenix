using System.Collections.Generic;
using System.Threading.Tasks;
using phoenix.Models;

namespace phoenix.Contracts
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetClientsAsync(string search);
        Task<ClientOverview> GetOverviewAsyc();
    }
}