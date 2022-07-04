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
    public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
      
        public OrganizationRepository() : base()
        {
            
        }

        public async Task CreateOrganizationAsync(Organization organization)
        {
            organization.OrganizationId = Guid.NewGuid();
            await dbSet.AddAsync(organization);
            await _yotorDatabase.SaveChangesAsync();
        }

        public async Task DeleteOrganizationAsync(Guid id)
        {
            var organization = await dbSet.Where(o => o.OrganizationId == id).FirstOrDefaultAsync();
            if (organization != null)
            {
                dbSet.Remove(organization);
                await _yotorDatabase.SaveChangesAsync();
            }
        }

        public async Task EditOrganizationAsync(Guid id, Organization organization)
        {
            var newOrganization = await dbSet.FirstOrDefaultAsync(o => o.OrganizationId == id);
            newOrganization.Name = organization.Name;
            newOrganization.Email = organization.Email;
            newOrganization.Phone = organization.Phone;
            newOrganization.Code = organization.Code;
            newOrganization.Taxes = organization.Taxes;
            newOrganization.Address = organization.Address;
            newOrganization.Founder = organization.Founder;
            newOrganization.Account = organization.Account;
            await _yotorDatabase.SaveChangesAsync();
            //_yotorDbContext.Entry(newOrganization).State = EntityState.Modified;
            //await _yotorDbContext.SaveChangesAsync();

        }

        public async Task<int> GetCountOfOrganizationsAsync()
        {
            return await dbSet.CountAsync();
        }

        public async Task<Organization> GetOrganizationByNameAsync(string name)
        {
            return await dbSet.Where(o => o.Name == name).FirstOrDefaultAsync();
        }

        public async Task<bool> IsOrganizationAsync(Guid id)
        {
            var organization = await dbSet.Where(o => o.OrganizationId == id).FirstOrDefaultAsync();
            if (organization != null) { return true; }
            return false;
        }


    }
}
