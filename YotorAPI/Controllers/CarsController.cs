using Business_Logic_Layer.Constructors;
using Business_Logic_Layer.Models;
using Caching.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace YotorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private Business_Logic_Layer.BLL.CarBLL _carBLL;
        private CachingManager _caching;
        private Guid userId => Guid.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public CarsController()
        {
            _carBLL = new Business_Logic_Layer.BLL.CarBLL(); 
            _caching = new CachingManager();
        }
        [HttpPost("GetTheNearestCar")]
        public async Task<IActionResult> GetTheNearestCar([FromBody]Point point)
        {
            try
            {
                return Ok(await _carBLL.GetTheNearestCar(point.Latitude,point.Longitude));
            }
            catch(Exception ex) { return StatusCode(500, ex.Message); }
        }
        [HttpGet]
        public async Task<IActionResult> GetCarsAsync()
        {
            try
            {
                var cars = _caching.GetCollectionFromCache(CollectionsType.Cars.ToString());
                if (cars == null)
                {
                    cars = await _carBLL.GetCarsAsync();
                    _caching.AddCollectionToCache(CollectionsType.Cars.ToString(), cars, DateTime.Now.AddMinutes(5));
                }
                return Ok(cars);
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarAsync(Guid id)
        {
            try
            {
                var car = await _carBLL.GetCarAsync(id);
                if (car != null) { return Ok(car); }
                return NotFound("Такого автомобиля нету");
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
        [HttpGet("Popular")]
        public async Task<IActionResult> GetMostPopularCarsAsync()
        {
            try
            {
                return Ok(await _carBLL.GetMostPopularCarsAsync());
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateCarAsync([FromBody] CarConstructor carConstructor)
        {
            var result = await _carBLL.CreateCarAsync(carConstructor, userId);
            if (result != "Ok") { return BadRequest(result); }
            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCarAsync(Guid id, [FromForm] CarConstructor carConstructor)
        {
            var result = await _carBLL.UpdateCarAsync(id, carConstructor, userId);
            if (result != "Ok") { return BadRequest(result); }
            return Ok(result);

        }
    }
}
