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
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        
        public BookingRepository() : base()
        {
            
        }
        public async Task CreateBookingAsync(Guid? restriction_id, Guid user_id, Guid car_id, Guid? feedback_id, DateTime start_date, DateTime end_date, bool status, int full_price, string start_address, string end_address)
        {
            var booking = new Booking();
            booking.BookingId = Guid.NewGuid();
            booking.RestrictionId = restriction_id;
            booking.UserId = user_id;
            booking.CarId = car_id;
            booking.FeedbackId = feedback_id;
            booking.StartDate = start_date;
            booking.EndDate = end_date;
            booking.Status = status;
            booking.FullPrice = full_price;
            booking.StartAddress = start_address;
            booking.EndAddress = end_address;
            await dbSet.AddAsync(booking);
            await _yotorDatabase.SaveChangesAsync();
        }

        public async Task<Booking> GetBookingAsync(Guid id)
        {
            return await dbSet.Where(b => b.BookingId == id).FirstOrDefaultAsync();
        }

        public async Task<Booking> GetBookingByParamsAsync(DateTime start_date, DateTime end_date, string start_address, string end_address)
        {
          
            return await dbSet.FirstOrDefaultAsync(b => b.StartDate == start_date & b.EndDate == end_date & b.StartAddress == start_address & b.EndAddress == end_address);

        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(Guid id)
        {
            return await dbSet.Where(b => b.UserId == id).ToListAsync();
        }
    }
}
