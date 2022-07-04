using Business_Logic_Layer.Constructors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private Business_Logic_Layer.BLL.BookingBLL _bookingBLL;
        private Guid UserId => Guid.Parse(User.Claims.Single(c => c.Type == "user_id").Value);

        public BookingsController()
        {
            _bookingBLL = new Business_Logic_Layer.BLL.BookingBLL();
        }


        [HttpGet]
        public async Task<IActionResult> GetBookingsAsync()
        {
            try
            {
                var bookings = await _bookingBLL.GetBookingsAsync(UserId);
                if (bookings != null) { return Ok(bookings); }
                return Unauthorized("Вы не являетесь администратором");
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingAsync(Guid id)
        {
            try
            {
                var bookings = await _bookingBLL.GetBookingAsync(id, UserId);
                if (bookings != null) { return Ok(bookings); }
                return Unauthorized("Вы не являетесь администратором");
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
        [HttpGet("User")]
        public async Task<IActionResult> GetBookingsByUserIdAsync()
        {
            try
            {
                return Ok(await _bookingBLL.GetBookingsByUserIdAsync(UserId));
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
        [HttpPost]
        public async Task<IActionResult> CreateBookingAsync([FromBody] BookingConstructor bookingConstructor)
        {

             var result = await _bookingBLL.CreateBookingAsync(bookingConstructor, UserId);
             if(result != "Ok") { return BadRequest(result); }
             return Ok(result);
        }


    }
}
