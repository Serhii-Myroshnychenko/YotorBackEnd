using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;
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
    public class OrganizationsController : ControllerBase
    {
        private Business_Logic_Layer.BLL.OrganizationBLL _organizationBLL;
        private Guid userId => Guid.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public OrganizationsController()
        {
            _organizationBLL = new Business_Logic_Layer.BLL.OrganizationBLL();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizationAsync(Guid id)
        {
            try
            {
                var result = await _organizationBLL.GetOrganizationAsync(id, userId);
                if (result != null) { return Ok(result); }
                return Unauthorized("Вы не являетесь администратором или такой организации нет");
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetOrganizationsAsync()
        {
            try
            {
                var result = await _organizationBLL.GetOrganizationsAsync();
                if (result != null) { return Ok(result); }
                return Unauthorized("Вы не являетесь администратором");
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrganizationAsync(OrganizationModel organization)
        {
            var result = await _organizationBLL.CreateOrganizationAsync(organization, userId);
            if(result != "Ok") { return BadRequest(result); }
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganizationAsync(Guid id, OrganizationModel organization)
        {
            var result = await _organizationBLL.UpdateOrganizationAsync(id, organization, userId);
            if(result != "Ok") { return BadRequest(result); }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizationAsync(Guid id)
        {
            var result = await _organizationBLL.DeleteOrganizationAsync(id);
            if (result != "Ok") { return BadRequest(result); }
            return Ok(result);
        }
    }
}
