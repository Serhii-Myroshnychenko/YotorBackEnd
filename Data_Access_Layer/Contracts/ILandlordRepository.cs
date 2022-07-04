using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Contracts
{
    public interface ILandlordRepository : IGenericRepository<Landlord>
    {
        Task UpdateLandlordAsync(Guid id, Landlord landlord);
        Task CreateLandlordAsync(Guid user_id, Guid organization_id, string name);
        Task<Landlord> IsLandlordAsync(Guid id);
        Task<Landlord> IsThisCarOfHisOrganizationAsync(string name);
    }
}
