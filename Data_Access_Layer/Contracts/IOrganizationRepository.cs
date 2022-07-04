using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Contracts
{
    public interface IOrganizationRepository : IGenericRepository<Organization>
    {
        Task EditOrganizationAsync(Guid id, Organization organization);
        Task CreateOrganizationAsync(Organization organization);
        Task<int> GetCountOfOrganizationsAsync();
        Task<bool> IsOrganizationAsync(Guid id);
        Task<Organization> GetOrganizationByNameAsync(string name);
    }
}
