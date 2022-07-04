using Data_Access_Layer.Contracts;
using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class RestrictionRepository : GenericRepository<Restriction>, IRestrictionRepository
    {
        public RestrictionRepository() : base()
        {

        }

        public async Task CreateRestrictionAsync(Guid landlord_id, string car_name, string description)
        {
            var restriction = new Restriction();
            restriction.RestrictionId = Guid.NewGuid();
            restriction.LandlordId = landlord_id;
            restriction.CarName = car_name;
            restriction.Description = description;
            await dbSet.AddAsync(restriction);
            await _yotorDatabase.SaveChangesAsync();
        }

        public async Task DeleteRestrictionAsync(Guid id)
        {
            var restriction = await dbSet.Where(r => r.RestrictionId == id).FirstOrDefaultAsync();
            if (restriction != null)
            {
                dbSet.Remove(restriction);
                await _yotorDatabase.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<Restriction>> GetRestrictionsOfSomePersonAsync(Guid id)
        {
            var restrictions = await (from Restriction in dbSet
                                      join Landlord in _yotorDatabase.Landlord on Restriction.LandlordId equals Landlord.LandlordId
                                      where
                                      Landlord.UserId == id
                                      select new Restriction
                                      {
                                          RestrictionId = Restriction.RestrictionId,
                                          LandlordId = Restriction.LandlordId,
                                          CarName = Restriction.CarName,
                                          Description = Restriction.Description
                                      }).ToListAsync();

            return restrictions;
        }

        public async Task UpdateRestrictionAsync(Guid id, Restriction restriction)
        {
            var newRestiction = await dbSet.FirstOrDefaultAsync(r => r.RestrictionId == id);
            newRestiction.LandlordId = restriction.LandlordId;
            newRestiction.CarName = restriction.CarName;
            newRestiction.Description = restriction.Description;
            await _yotorDatabase.SaveChangesAsync();
        }

        public async Task<Restriction> GetRestrictionByCarNameAsync(string name)
        {
            return await dbSet.Where(r => r.CarName == name).FirstOrDefaultAsync();
        }
    }
}
