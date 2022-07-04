using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Contracts
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task CreateBookingAsync(Guid? restriction_id, Guid user_id, Guid car_id, Guid? feedback_id, DateTime start_date, DateTime end_date, bool status, int full_price, string start_address, string end_address);

        Task<Booking> GetBookingByParamsAsync(DateTime start_date, DateTime end_date, string start_address, string end_address);
        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(Guid id);
    }
}
