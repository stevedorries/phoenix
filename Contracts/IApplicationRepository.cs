using System.Collections.Generic;
using System.Threading.Tasks;
using phoenix.Models;

namespace phoenix.Contracts
{
    public interface IApplicationRepository
    {
        Task<ApplicationOverview> GetOverviewAsync();
        Task<IEnumerable<Application>> GetApplicationsAsync(string search);
    }
}