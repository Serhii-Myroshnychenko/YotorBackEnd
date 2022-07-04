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
    public class UsersController : ControllerBase
    {
        private Business_Logic_Layer.BLL.UserBLL _userBLL;
        private Guid userId => Guid.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public UsersController()
        {
            _userBLL = new Business_Logic_Layer.BLL.UserBLL();
        }
        [HttpGet("Info")]
        public async Task<IActionResult> GetCustomerByIdAsync()
        {
            try
            {
                var result = await _userBLL.GetCustomerByIdAsync(userId);
                if (result != null) { return Ok(result); }
                return BadRequest();
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
        [HttpGet("IsAdmin")]
        public async Task<IActionResult> IsAdminAsync()
        {
            try
            {
                return Ok(await _userBLL.IsAdminAsync(userId));
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
    }
}
