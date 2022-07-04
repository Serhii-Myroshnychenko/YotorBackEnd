using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Contracts
{
    public interface ICarRepository : IGenericRepository<Car>
    {

        Task CreateCarAsync(Guid organization_id, string model, string brand, string year, string transmission, string address,string city, bool status, string type, int price, string phote, string description, string number);
        Task UpdateCarAsync(Guid id, string model, string brand, string year, string transmission, string address,string city, bool status, string type, int price, string phote, string description, string number);
        Task<IEnumerable<Car>> GetMostPopularCarsAsync();
        Task<Car> GetCarByCarNameAsync(string name);
        Task UpdateStatusCarAsync(Guid id);

    }
}
