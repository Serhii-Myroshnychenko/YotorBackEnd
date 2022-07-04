using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private Business_Logic_Layer.BLL.UserBLL _userBLL;
        private Guid userId => Guid.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public LoginController()
        {
            _userBLL = new Business_Logic_Layer.BLL.UserBLL();
        }
        [HttpGet("IsAdmin")]
        public async Task<IActionResult> IsAdmin()
        {
            try
            {
                return Ok(await _userBLL.IsAdminAsync(userId));
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
        [HttpGet("Info")]
        public async Task<IActionResult> GetCustomerByIdAsync()
        {
            try
            {
                return Ok(await _userBLL.GetCustomerByIdAsync(userId));
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
    }
}
