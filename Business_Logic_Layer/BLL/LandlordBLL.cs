using AutoMapper;
using Business_Logic_Layer.Constructors;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.BLL
{
    public class LandlordBLL
    {
        private readonly Data_Access_Layer.Configuration.IUnitOfWork _iUnitOfWorkDAL;
        private readonly Mapper _landlordMapper;

        public LandlordBLL()
        {
            _iUnitOfWorkDAL = new Data_Access_Layer.Configuration.UnitOfWork();
            _landlordMapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Landlord, LandlordModel>().ReverseMap()));
        }

        public async Task<bool> IsLandlordLoginAsync(Guid userId)
        {
            var landlord = await _iUnitOfWorkDAL.Landlords.IsLandlordAsync(userId);
            if(landlord != null) { return true; }
            return false;
        }

        public async Task<IEnumerable<LandlordModel>> GetLandlordsAsync(Guid userId)
        {
            IEnumerable<LandlordModel> landlordModels = null;
            bool isAdmin = await _iUnitOfWorkDAL.Customers.IsAdminAsync(userId);
            if (isAdmin == true)
            {
                var landlords = await _iUnitOfWorkDAL.Landlords.GetAllAsync();
                landlordModels = _landlordMapper.Map<IEnumerable<Landlord>, IEnumerable<LandlordModel>>(landlords);
            }
            return landlordModels;
        }

        public async Task<LandlordModel> GetLandlordAsync(Guid id, Guid userId)
        {
             LandlordModel landlordModel = null;
             bool isAdmin = await _iUnitOfWorkDAL.Customers.IsAdminAsync(userId);
             if (isAdmin == true)
             {
                 var landlord = await _iUnitOfWorkDAL.Landlords.GetByIdAsync(id);
                 landlordModel = _landlordMapper.Map<Landlord, LandlordModel>(landlord);
             }
             return landlordModel;
        }

        public async Task<string> CreateLandlordAsync(LandlordConstructor landlordConstructor, Guid userId)
        {   try
            {
                bool isAdmin = await _iUnitOfWorkDAL.Customers.IsAdminAsync(userId);
                if (isAdmin == true)
                {
                    var organizByName = await _iUnitOfWorkDAL.Organizations.GetOrganizationByNameAsync(landlordConstructor.OrganizationName);
                    var customerByName = await _iUnitOfWorkDAL.Customers.GetCustomerByNameAsync(landlordConstructor.CustomerName);
                    if (organizByName != null && customerByName != null)
                    {
                        var isUserMemberOfTheOrganization = await _iUnitOfWorkDAL.Landlords.IsLandlordAsync(customerByName.UserId);
                        bool isUser = await _iUnitOfWorkDAL.Customers.IsUserAsync(customerByName.UserId);
                        bool isOrganization = await _iUnitOfWorkDAL.Organizations.IsOrganizationAsync(organizByName.OrganizationId);
                        if (isUser == true && isOrganization == true && isUserMemberOfTheOrganization == null)
                        {
                            await _iUnitOfWorkDAL.Landlords.CreateLandlordAsync(customerByName.UserId, organizByName.OrganizationId, customerByName.FullName);
                            return "Ok";
                        }
                        else
                        {
                            return "Данные не являются корректными";
                        }
                    }
                    else
                    {
                        return "Что-то пошло не так";
                    }
                }
                else
                {
                    return "Вы не являетесь администатором";
                }
            }
            catch (Exception ex) { return ex.Message; }
        } 

        public async Task<string> UpdateLandlordAsync(Guid id, Landlord landlord,Guid userId)
        {
            try
            {
                bool isAdmin = await _iUnitOfWorkDAL.Customers.IsAdminAsync(userId);
                if (isAdmin == true)
                {
                    bool isUser = await _iUnitOfWorkDAL.Customers.IsUserAsync(landlord.UserId);
                    bool isOrganization = await _iUnitOfWorkDAL.Organizations.IsOrganizationAsync(landlord.OrganizationId);
                    if (isUser == true && isOrganization == true)
                    {
                        await _iUnitOfWorkDAL.Landlords.UpdateLandlordAsync(id, landlord);
                        return "Ok";
                    }
                    else
                    {
                        return "Данные не являются корректными";
                    }
                }
                else
                {
                    return "Вы не являетесь администратором";
                }
            }
            catch (Exception ex) { return ex.Message; }
        }
    }
}
