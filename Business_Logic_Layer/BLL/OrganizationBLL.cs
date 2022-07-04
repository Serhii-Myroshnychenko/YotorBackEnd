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
    public class OrganizationBLL
    {
        private readonly Data_Access_Layer.Configuration.IUnitOfWork _iUnitOfWorkDAL;
        private readonly Mapper _organizationMapper;

        public OrganizationBLL()
        {
            _iUnitOfWorkDAL = new Data_Access_Layer.Configuration.UnitOfWork();
            _organizationMapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Organization, OrganizationModel>().ReverseMap()));
        }
        public async Task<OrganizationModel> GetOrganizationAsync(Guid id, Guid userId)
        {
            OrganizationModel organizationModel = null;
            var admin = await _iUnitOfWorkDAL.Customers.IsAdminAsync(userId);
            if (admin == true)
            {
                var organization = await _iUnitOfWorkDAL.Organizations.GetByIdAsync(id);
                organizationModel = _organizationMapper.Map<Organization, OrganizationModel>(organization);
            }
            return organizationModel;
        }

        public async Task<IEnumerable<OrganizationModel>> GetOrganizationsAsync()
        {
            IEnumerable<OrganizationModel> organizationModels = null;
            var organizations = await _iUnitOfWorkDAL.Organizations.GetAllAsync();
            organizationModels = _organizationMapper.Map<IEnumerable<Organization>, IEnumerable<OrganizationModel>>(organizations);
            return organizationModels;
        }
        public async Task<string> CreateOrganizationAsync(OrganizationModel organizationModel, Guid userId)
        {
            try
            {
                var admin = await _iUnitOfWorkDAL.Customers.IsAdminAsync(userId);
                if (admin == true)
                {
                    var organization = _organizationMapper.Map<OrganizationModel, Organization>(organizationModel);
                    await _iUnitOfWorkDAL.Organizations.CreateOrganizationAsync(organization);
                    return "Ok";
                }
                else
                {
                    return "Вы не являетесь администратором";
                }

            }
            catch(Exception ex) { return ex.Message; }
            
        }
        public async Task<string> UpdateOrganizationAsync(Guid id, OrganizationModel organizationModel, Guid userId)
        {
            try
            {
                var admin = await _iUnitOfWorkDAL.Customers.IsAdminAsync(userId);
                if (admin == true)
                {
                    var organization = _organizationMapper.Map<OrganizationModel, Organization>(organizationModel);
                    await _iUnitOfWorkDAL.Organizations.EditOrganizationAsync(id, organization);
                    return "Ok";
                }
                else
                {
                    return "Вы не являетесь администратором";
                }
            }
            catch(Exception ex) { return ex.Message; }
   
        }

        public async Task<string> DeleteOrganizationAsync(Guid id)
        {
            try
            {
                await _iUnitOfWorkDAL.Organizations.DeleteAsync(id);
                return "Ok";
            }
            catch(Exception ex) { return ex.Message; }
        }

    }
}
