using AutoMapper;
using Business_Logic_Layer.Constructors;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Utils;
using Data_Access_Layer.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Business_Logic_Layer.Utils.TokenJWT;

namespace Business_Logic_Layer.BLL
{
    public class UserBLL
    {
        private readonly Data_Access_Layer.Configuration.IUnitOfWork _iUnitOfWorkDAL;
        private readonly Mapper _customerMapper;

        public UserBLL()
        {
            _iUnitOfWorkDAL = new Data_Access_Layer.Configuration.UnitOfWork();
            _customerMapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Customer, CustomerModel>().ReverseMap()));
        }
        public async Task<CustomerModel> GetCustomerByIdAsync(Guid UserId)
        {
            CustomerModel customerModel = null;
            var customer = await _iUnitOfWorkDAL.Customers.GetCustomerByIdAsync(UserId);
            if (customer != null)
            {
                customerModel = _customerMapper.Map<Customer, CustomerModel>(customer);

            }
            return customerModel;
            
        }

        public async Task<bool> IsAdminAsync(Guid UserId)
        {

             return await _iUnitOfWorkDAL.Customers.IsAdminAsync(UserId);       

        }
        public async Task<IEnumerable<CustomerModel>> GetCustomersAsync()
        {
            IEnumerable<CustomerModel> customerModels = null;
            var customers = await _iUnitOfWorkDAL.Customers.GetAllAsync();
            if (customers != null)
            {
                customerModels = _customerMapper.Map<IEnumerable<Customer>, IEnumerable<CustomerModel>>(customers);
            }
            return customerModels;
        }

        public async Task<string> RegistrationAsync(string full_name, string email, string phone, string password)
        {
            try
            {
                await _iUnitOfWorkDAL.Customers.RegistrationAsync(full_name,email, phone, password, false);
                await _iUnitOfWorkDAL.CompleteAsync();
                return "Ok";
            }
            catch (Exception ex) { return ex.InnerException.Message; }
            
        }

        public async Task<string> LoginAsync(Login login, IOptions<AuthOptions> _options)
        {
            try
            {
                var customer = await _iUnitOfWorkDAL.Customers.GetCustomerAsync(login.Email, login.Password);
                if (customer != null)
                {
                    var token = GenerateJWT(customer,_options);
                    return token;
                }
                return "Неверный логин или пароль";
            }
            catch (Exception ex) { return ex.Message; }
            
        }
    }
}
