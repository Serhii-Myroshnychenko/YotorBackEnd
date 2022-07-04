using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Contracts
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> GetCustomerAsync(string email, string password);
        Task RegistrationAsync(string full_name, string email, string phone, string password, bool is_admin);
        Task<bool> IsAdminAsync(Guid id);
        Task<bool> IsUserAsync(Guid id);
        Task<Customer> GetCustomerByNameAsync(string name);
        Task<Customer> GetCustomerByIdAsync(Guid id);
    }
}
