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
    public class RestrictionsController : ControllerBase
    {
        private Business_Logic_Layer.BLL.RestrictionBLL _restrictionBLL;
        private Guid userId => Guid.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public RestrictionsController()
        {
            _restrictionBLL = new Business_Logic_Layer.BLL.RestrictionBLL();
        }
        [HttpGet]
        public async Task<IActionResult> GetRestrictionsAsync()
        {
            try
            {
                var result = await _restrictionBLL.GetRestrictionsAsync();
                if (result != null) { return Ok(result); }
                return BadRequest();
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }

        }
        [HttpGet("Person")]
        public async Task<IActionResult> GetRestictionsOfSomePerson([FromHeader] Guid id)
        {
            try
            {
                var result = await _restrictionBLL.GetRestictionsOfSomePerson(id);
                if (result != null) { return Ok(result); }
                return BadRequest();
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestrctionByIdAsync(Guid id)
        {
            try
            {
                return Ok(await _restrictionBLL.GetRestrictionByIdAsync(id));
            }
            catch(Exception ex) { return StatusCode(500, ex.Message); }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestrictionAsync( Guid id)
        {

            var result = await _restrictionBLL.DeleteRestrictionAsync(id);
            if (result != "Ok") { return BadRequest(); }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRestrictionAsync([FromBody] RestrictionConstructor restrictionConstructor)
        {
            var result = await _restrictionBLL.CreateRestrictionAsync(restrictionConstructor);
            if (result != "Ok") { return BadRequest(result); }
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestrictionAsync(Guid id, RestrictionConstructor restrictionConstructor)
        {
            var result = await _restrictionBLL.UpdateRestrictionAsync(userId, id, restrictionConstructor);
            if(result!="Ok") { return BadRequest(result); }
            return Ok(result);
        }
    }
}
