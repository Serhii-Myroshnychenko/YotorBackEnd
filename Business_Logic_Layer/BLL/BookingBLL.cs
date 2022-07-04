using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Utils;
using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Mvc;

using Business_Logic_Layer.Constructors;

namespace Business_Logic_Layer.BLL
{
    public class BookingBLL
    {
        private readonly Data_Access_Layer.Configuration.IUnitOfWork _iUnitOfWorkDAL;
        private readonly Mapper _bookingMapper;

        public BookingBLL()
        {
            _iUnitOfWorkDAL = new Data_Access_Layer.Configuration.UnitOfWork();
            _bookingMapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingModel>().ReverseMap()));
        }

        public async Task<IEnumerable<BookingModel>> GetBookingsAsync(Guid userId)
        {
             IEnumerable<BookingModel> bookingModels = null;
             bool isAdmin = await _iUnitOfWorkDAL.Customers.IsAdminAsync(userId);
             if (isAdmin == true)
             {
                var bookings = await _iUnitOfWorkDAL.Bookings.GetAllAsync();
                bookingModels = _bookingMapper.Map<IEnumerable<Booking>, IEnumerable<BookingModel>>(bookings);
             }
             return bookingModels;
        }
        
        public async Task<BookingModel> GetBookingAsync(Guid id, Guid userId)
        {
             BookingModel bookingModel = null;
             bool isAdmin = await _iUnitOfWorkDAL.Customers.IsAdminAsync(userId);
             if (isAdmin == true)
             {
                  var booking = await _iUnitOfWorkDAL.Bookings.GetByIdAsync(id);
                  if (booking != null)
                  {
                        bookingModel = _bookingMapper.Map<Booking, BookingModel>(booking);
                  }
             }
             return bookingModel;   
        }

        
        public async Task<IEnumerable<BookingModel>> GetBookingsByUserIdAsync(Guid userId)
        {
            IEnumerable<BookingModel> bookingModels = null;
            var bookings = await _iUnitOfWorkDAL.Bookings.GetBookingsByUserIdAsync(userId);
            bookingModels = _bookingMapper.Map<IEnumerable<Booking>, IEnumerable<BookingModel>>(bookings);
            return bookingModels;
        }
       
        public async Task<string> CreateBookingAsync(BookingConstructor bookingConstructor, Guid userId)
        {
            try
            {
                var restriction = await _iUnitOfWorkDAL.Restrictions.GetRestrictionByCarNameAsync(bookingConstructor.CarName);
                var car = await _iUnitOfWorkDAL.Cars.GetCarByCarNameAsync(bookingConstructor.CarName);
                int countOfDays = bookingConstructor.EndDate.Day - bookingConstructor.StartDate.Day;
                double coefficient = BookingСoefficient.CalculateСoefficient(countOfDays);
                int totalPrice = (int)(bookingConstructor.FullPrice * countOfDays * coefficient);

                if (car != null && restriction != null)
                {
                    await _iUnitOfWorkDAL.Bookings.CreateBookingAsync(restriction.RestrictionId, userId, car.CarId, null, bookingConstructor.StartDate, bookingConstructor.EndDate, false, totalPrice, bookingConstructor.StartAddress, bookingConstructor.EndAddress);
                    await _iUnitOfWorkDAL.Cars.UpdateStatusCarAsync(car.CarId);
                    await _iUnitOfWorkDAL.CompleteAsync();
                    return "Ok";
                }
                else if (car != null && restriction == null)
                {
                    await _iUnitOfWorkDAL.Bookings.CreateBookingAsync(null, userId, car.CarId, null, bookingConstructor.StartDate, bookingConstructor.EndDate, false, totalPrice, bookingConstructor.StartAddress, bookingConstructor.EndAddress);
                    await _iUnitOfWorkDAL.Cars.UpdateStatusCarAsync(car.CarId);
                    await _iUnitOfWorkDAL.CompleteAsync();
                    return "Ok";
                }
                else
                {
                    return "Что-то пошло нет так";
                }
            }
            catch(Exception ex) { return ex.Message; }
        }

    }
}
