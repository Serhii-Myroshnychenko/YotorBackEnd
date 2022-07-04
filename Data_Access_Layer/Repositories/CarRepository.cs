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
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        public CarRepository() : base()
        {
            
        }

        public async Task CreateCarAsync(Guid organization_id, string model, string brand, string year, string transmission, string address,string city, bool status, string type, int price, string photo, string description, string number)
        {
            var car = new Car();
            car.CarId = Guid.NewGuid();
            car.OrganizationId = organization_id;
            car.Model = model;
            car.Brand = brand;
            car.Year = year;
            car.Transmission = transmission;
            car.Address = address;
            car.City = city;
            car.Status = status;
            car.Type = type;
            car.Price = price;
            car.Photo = photo;
            car.Description = description;
            car.Number = number;

            await dbSet.AddAsync(car);
            await _yotorDatabase.SaveChangesAsync();
        }

        public async Task UpdateCarAsync(Guid id, string model, string brand, string year, string transmission, string address,string city, bool status, string type, int price, string photo, string description, string number)
        {
            var car = await dbSet.FirstOrDefaultAsync(c => c.CarId == id);
            car.Model = model;
            car.Brand = brand;
            car.Year = year;
            car.Transmission = transmission;
            car.Address = address;
            car.City = city;
            car.Status = status;
            car.Type = type;
            car.Price = price;
            car.Photo = photo;
            car.Description = description;
            car.Number = number;
            await _yotorDatabase.SaveChangesAsync();
                
        }
        public async Task<IEnumerable<Car>> GetMostPopularCarsAsync()
        {
            return await dbSet.FromSqlRaw("select Car.car_id, Car.organization_id, Car.model, Car.brand, Car.year, Car.transmission, Car.address, Car.city, Car.status, Car.type, Car.price, Car.photo, Car.description, Car.number from Car left join Booking on Car.car_id = Booking.car_id group by Car.car_id, Car.organization_id, Car.model, Car.brand, Car.year, Car.transmission, Car.address,Car.city, Car.status, Car.type, Car.price, Car.photo, Car.description, Car.number having count(Booking.booking_id) >= 0 order by COUNT(Booking.booking_id) desc").ToListAsync();
        }
        public async Task<Car> GetCarByCarNameAsync(string name)
        {
            return await dbSet.Where(c => c.Model == name && c.Status == true).FirstOrDefaultAsync();
        }
        public async Task UpdateStatusCarAsync(Guid id)
        {
            var car = await dbSet.FirstOrDefaultAsync(c => c.CarId == id);
            car.Status = false;
            _yotorDatabase.Entry(car).State = EntityState.Modified;
            await _yotorDatabase.SaveChangesAsync();
        }
    }
}
