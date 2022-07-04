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
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        
        public CustomerRepository() : base()
        {
            
        }
        public async Task<Customer> GetCustomerAsync(string email, string password)
        {
            var customer = await dbSet.Where(c => c.Email == email).FirstOrDefaultAsync();
            if (customer != null)
            {
                bool isValid = BCrypt.Net.BCrypt.Verify(password, customer.Password);
                if (isValid)
                {
                    return customer;
                }
            }
            return null;
        }


        public async Task RegistrationAsync(string full_name, string email, string phone, string password, bool is_admin)
        {
            var customer = new Customer();
            customer.UserId = Guid.NewGuid();
            customer.FullName = full_name;
            customer.Email = email;
            customer.Phone = phone;
            customer.Password = BCrypt.Net.BCrypt.HashPassword(password);
            customer.IsAdmin = is_admin;

            await dbSet.AddAsync(customer);
            await _yotorDatabase.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            return await dbSet.Where(c => c.UserId == id).FirstOrDefaultAsync();
        }

        public async Task<Customer> GetCustomerByNameAsync(string name)
        {
            return await dbSet.Where(c => c.FullName == name).FirstOrDefaultAsync();
        }
        public async Task<bool> IsAdminAsync(Guid id)
        {
            var user = await dbSet.Where(c => c.UserId == id && c.IsAdmin == true).FirstOrDefaultAsync();
            if (user != null) { return true; }
            return false;
        }
        public async Task<bool> IsUserAsync(Guid id)
        {
            var user = await dbSet.Where(c => c.UserId == id).FirstOrDefaultAsync();
            if (user != null) { return true; }
            return false;
        }
    }
}
