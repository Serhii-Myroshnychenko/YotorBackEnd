using Business_Logic_Layer.Constructors;
using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Authorization;
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
    public class LandlordsController : ControllerBase
    {
        private Business_Logic_Layer.BLL.LandlordBLL _landlordBLL;
        private Guid userId => Guid.Parse(User.Claims.Single(c => c.Type == "user_id").Value);

        public LandlordsController()
        {
            _landlordBLL = new Business_Logic_Layer.BLL.LandlordBLL();
        }
        [HttpGet("IsLandlord")]
        public async Task<IActionResult> IsLandlordLoginAsync()
        {
            try { return Ok(await _landlordBLL.IsLandlordLoginAsync(userId)); }
            catch(Exception ex) { return StatusCode(500, ex.Message); }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetLandlordsAsync()
        {
            try
            {
                var result = await _landlordBLL.GetLandlordsAsync(userId);
                if (result != null) { return Ok(result); }
                return Unauthorized("Вы не являетесь администратором");
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetLandlordAsync(Guid id)
        {
            try
            {
                var result = await _landlordBLL.GetLandlordAsync(id, userId);
                if (result != null) { return Ok(result); }
                return Unauthorized("Вы не являетесь администратором");
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateLandlordAsync([FromForm] LandlordConstructor landlordConstructor)
        {
            var result = await _landlordBLL.CreateLandlordAsync(landlordConstructor, userId);
            if (result != "Ok") { return BadRequest(result); }
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateLandlordAsync(Guid id, Landlord landlord)
        {
            var result = await _landlordBLL.UpdateLandlordAsync(id, landlord, userId);
            if (result != "Ok") { return BadRequest(result); }
            return Ok(result);
        }
    }
}
