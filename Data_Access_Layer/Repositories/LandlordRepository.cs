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
    public class LandlordRepository : GenericRepository<Landlord>,ILandlordRepository
    {
        
        public LandlordRepository() : base()
        {
            
        }

        public async Task CreateLandlordAsync(Guid user_id, Guid organization_id, string name)
        {
            var landlord = new Landlord();
            landlord.LandlordId = Guid.NewGuid();
            landlord.UserId = user_id;
            landlord.OrganizationId = organization_id;
            landlord.Name = name;
            await dbSet.AddAsync(landlord);
            await _yotorDatabase.SaveChangesAsync();
        }


        public async Task UpdateLandlordAsync(Guid id, Landlord landlord)
        {
            var newLandlord = await dbSet.FirstOrDefaultAsync(l => l.LandlordId == id);
            newLandlord.UserId = landlord.UserId;
            newLandlord.OrganizationId = landlord.OrganizationId;
            newLandlord.Name = landlord.Name;

            await _yotorDatabase.SaveChangesAsync();
            //_yotorDbContext.Entry(newLandlord).State = EntityState.Modified;
            //await _yotorDbContext.SaveChangesAsync();
        }

        public async Task<Landlord> IsLandlordAsync(Guid id)
        {
            return await dbSet.Where(l => l.UserId == id).FirstOrDefaultAsync();
        }

        public async Task<Landlord> IsThisCarOfHisOrganizationAsync(string name)
        {
            var landlord = await (from Car in _yotorDatabase.Car
                                  join Landlord in _yotorDatabase.Landlord on Car.OrganizationId equals Landlord.OrganizationId
                                  where
                                  Car.Model == name
                                  select new Landlord
                                  {
                                      LandlordId = Landlord.LandlordId,
                                      UserId = Landlord.UserId,
                                      OrganizationId = Landlord.OrganizationId,
                                      Name = Landlord.Name
                                  }).FirstOrDefaultAsync();

            return landlord;
        }

    }
}
