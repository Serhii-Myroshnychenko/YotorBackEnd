using Business_Logic_Layer.Constructors;
using Business_Logic_Layer.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IOptions<AuthOptions> _options;
        private Business_Logic_Layer.BLL.UserBLL _userBLL;
        public CustomersController(IOptions<AuthOptions> options)
        {
            _userBLL = new Business_Logic_Layer.BLL.UserBLL();
            _options = options;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            try
            {
                return Ok(await _userBLL.GetCustomersAsync());
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }

        }
        [HttpPost("Registration")]
        public async Task<IActionResult> RegistrationAsync([FromBody] Registration registration)
        {
            var result = await _userBLL.RegistrationAsync(registration.Full_name, registration.Email, registration.Phone, registration.Password);
            if (result != "Ok")
            {
                return BadRequest(result);
            }
            return Ok(result);

        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] Login login)
        {
             var token = await _userBLL.LoginAsync(login, _options);
             if (token != "Неверный логин или пароль")
             {
                 return Ok(new
                 {
                      access_token = token
                 });
             }
             return Ok(token);
        }
    }
}
