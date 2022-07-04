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
    public class RestrictionBLL
    {
        private readonly Data_Access_Layer.Configuration.IUnitOfWork _iUnitOfWorkDAL;
        private readonly Mapper _restrictionMapper;

        public RestrictionBLL()
        {
            _iUnitOfWorkDAL = new Data_Access_Layer.Configuration.UnitOfWork();
            _restrictionMapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Restriction, RestrictionModel>().ReverseMap()));
        }

        public async Task<IEnumerable<RestrictionModel>> GetRestrictionsAsync()
        {
            IEnumerable<RestrictionModel> restrictionModels = null;
            var restrictions = await _iUnitOfWorkDAL.Restrictions.GetAllAsync();
            restrictionModels = _restrictionMapper.Map<IEnumerable<Restriction>, IEnumerable<RestrictionModel>>(restrictions);
            return restrictionModels;

        }
        public async Task<IEnumerable<RestrictionModel>> GetRestictionsOfSomePerson(Guid id)
        {
            IEnumerable<RestrictionModel> restrictionModels = null;
            var restrictions = await _iUnitOfWorkDAL.Restrictions.GetRestrictionsOfSomePersonAsync(id);
            restrictionModels = _restrictionMapper.Map<IEnumerable<Restriction>, IEnumerable<RestrictionModel>>(restrictions);
            return restrictionModels;
        }
        public async Task<RestrictionModel> GetRestrictionByIdAsync(Guid id)
        {
            RestrictionModel restrictionModel = null;
            var restrition = await _iUnitOfWorkDAL.Restrictions.GetByIdAsync(id);
            restrictionModel = _restrictionMapper.Map<Restriction, RestrictionModel>(restrition);
            return restrictionModel;
        }

        public async Task<string> DeleteRestrictionAsync(Guid Id)
        {
            try
            {
                await _iUnitOfWorkDAL.Restrictions.DeleteRestrictionAsync(Id);
                return "Ok";
            }
            catch(Exception ex) { return ex.Message; }

        }
        public async Task<string> CreateRestrictionAsync(RestrictionConstructor restrictionConstructor)
        {
            try
            {
                var landlord = await _iUnitOfWorkDAL.Landlords.IsLandlordAsync(Guid.Parse(restrictionConstructor.UserId));
                var isHisOrgan = await _iUnitOfWorkDAL.Landlords.IsThisCarOfHisOrganizationAsync(restrictionConstructor.Name);
                if (landlord != null && isHisOrgan != null && landlord.OrganizationId == isHisOrgan.OrganizationId)
                {
                    await _iUnitOfWorkDAL.Restrictions.CreateRestrictionAsync(landlord.LandlordId, restrictionConstructor.Name, restrictionConstructor.Description);
                    return "Ok";
                }
                else
                {
                    return "У вас нету доступа к данному автомобилю";
                }
            }
            catch (Exception ex) { return ex.Message; }
        }
        public async Task<string> UpdateRestrictionAsync(Guid userId,Guid id, RestrictionConstructor restrictionConstructor)
        {
            try
            {
                var admin = await _iUnitOfWorkDAL.Customers.IsAdminAsync(userId);
                if (admin == true)
                {
                    var landlord = await _iUnitOfWorkDAL.Landlords.IsLandlordAsync(Guid.Parse(restrictionConstructor.UserId));
                    var isHisOrgan = await _iUnitOfWorkDAL.Landlords.IsThisCarOfHisOrganizationAsync(restrictionConstructor.Name);
                    if (landlord != null && isHisOrgan != null && landlord.OrganizationId == isHisOrgan.OrganizationId)
                    {
                        var restriction = new Restriction();
                        restriction.RestrictionId = id;
                        restriction.LandlordId = landlord.LandlordId;
                        restriction.Description = restrictionConstructor.Description;
                        restriction.CarName = restrictionConstructor.Name;
                        await _iUnitOfWorkDAL.Restrictions.UpdateRestrictionAsync(id,restriction);
                        return "Ok";
                    }
                    else
                    {
                        return "У этого арендодателя нету доступа к данному автомобилю";
                    }
                }
                else
                {
                    return "У вас недостаточно прав";
                }
            }
            catch(Exception ex) { return ex.Message; }
        }

    }
}
