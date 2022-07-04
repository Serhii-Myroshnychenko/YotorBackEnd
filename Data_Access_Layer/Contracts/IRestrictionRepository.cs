using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Contracts
{
    public interface IRestrictionRepository : IGenericRepository<Restriction>
    {

        Task CreateRestrictionAsync(Guid landlord_id, string car_name, string description);
        Task UpdateRestrictionAsync(Guid id, Restriction restriction);
        Task DeleteRestrictionAsync(Guid id);
        Task<IEnumerable<Restriction>> GetRestrictionsOfSomePersonAsync(Guid id);
        Task<Restriction> GetRestrictionByCarNameAsync(string name);
    }
}
