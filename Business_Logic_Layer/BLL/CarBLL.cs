using AutoMapper;
using Business_Logic_Layer.Constructors;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Utils;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.BLL
{
    public class CarBLL
    {
        private readonly Data_Access_Layer.Configuration.IUnitOfWork _iUnitOfWorkDAL;
        private readonly Mapper _carMapper;

        public CarBLL()
        {
            _iUnitOfWorkDAL = new Data_Access_Layer.Configuration.UnitOfWork();
            _carMapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Car, CarModel>().ReverseMap()));
        }

        public async Task<IEnumerable<CarModel>> GetCarsAsync()
        {
            IEnumerable<CarModel> carModels = null;
            var cars = await _iUnitOfWorkDAL.Cars.GetAllAsync();
            carModels = _carMapper.Map<IEnumerable<Car>, IEnumerable<CarModel>>(cars);
            return carModels;
        }
       
        public async Task<CarModel> GetCarAsync(Guid id)
        {

            CarModel carModel = null;
            var car = await _iUnitOfWorkDAL.Cars.GetByIdAsync(id);
            carModel = _carMapper.Map<Car, CarModel>(car);
            return carModel;
        }
        
        public async Task<IEnumerable<CarModel>> GetMostPopularCarsAsync()
        {
            IEnumerable<CarModel> carModels = null;
            var cars = await _iUnitOfWorkDAL.Cars.GetMostPopularCarsAsync();
            carModels = _carMapper.Map<IEnumerable<Car>, IEnumerable<CarModel>>(cars);
            return carModels;

        }

        public async Task<string> CreateCarAsync(CarConstructor carConstructor,Guid userId)
        {
            try
            {
                var isLandlord = await _iUnitOfWorkDAL.Landlords.IsLandlordAsync(userId);
                if (isLandlord != null)
                {
                    await _iUnitOfWorkDAL.Cars.CreateCarAsync(isLandlord.OrganizationId, carConstructor.Model, carConstructor.Brand, carConstructor.Year, carConstructor.Transmission, carConstructor.Address, carConstructor.City, true, carConstructor.Type, carConstructor.Price, carConstructor.Photo, carConstructor.Description, carConstructor.Number);
                    return "Ok";
                }
                return "Что-то пошло не так";
            }
            catch(Exception ex) { return ex.InnerException.Message; }
            
        }

        public async Task<string> UpdateCarAsync(Guid id,  CarConstructor carConstructor, Guid userId)
        {
            try
            {
                bool isAdmin = await _iUnitOfWorkDAL.Customers.IsAdminAsync(userId);
                if (isAdmin == true)
                {

                    await _iUnitOfWorkDAL.Cars.UpdateCarAsync(id, carConstructor.Model, carConstructor.Brand, carConstructor.Year, carConstructor.Transmission, carConstructor.Address, carConstructor.City, carConstructor.Status, carConstructor.Type, carConstructor.Price, null, carConstructor.Description, carConstructor.Number);
                    return "Успешно";
                }
                return "Недостаточно прав";
            }
            catch(Exception ex) { return ex.Message; }
        }

        public async Task<CarModel> GetTheNearestCar(string latitude, string longitude)
        {
            IEnumerable<CarModel> carModels = null;
            var cars = await _iUnitOfWorkDAL.Cars.GetAllAsync();
            carModels = _carMapper.Map<IEnumerable<Car>, IEnumerable<CarModel>>(cars);
            var location = CarSelection.GetUsersLocationFromLatitudeAndLongitude(latitude, longitude);
            return CarSelection.GetTheNearestCar(carModels, location);
        }
    }
}
