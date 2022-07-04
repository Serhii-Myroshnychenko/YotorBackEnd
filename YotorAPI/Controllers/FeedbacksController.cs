using Business_Logic_Layer.Constructors;
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
    public class FeedbacksController : ControllerBase
    {
        private Business_Logic_Layer.BLL.FeedbackBLL _feedbackBLL;
        private Guid userId => Guid.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public FeedbacksController()
        {
            _feedbackBLL = new Business_Logic_Layer.BLL.FeedbackBLL();
        }
        [HttpGet]
        [Authorize]

        public async Task<IActionResult> GetFeedbacksAsync()
        {
            try
            {
                return Ok(await _feedbackBLL.GetFeedbacksAsync());
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetFeedbackAsync(Guid id)
        {
            try
            {
                var result = await _feedbackBLL.GetFeedbackAsync(id, userId);
                if (result != null) { return Ok(result); }
                return Unauthorized("Вы не являетесь администратором"); 
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateFeedbackAsync([FromBody] FeedbackConstructor feedbackConstructor)
        {
            var result = await _feedbackBLL.CreateFeedbackAsync(feedbackConstructor, userId);
            if (result != "Ok") { return BadRequest(result); }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedbackAsync(Guid id)
        {
            var result = await _feedbackBLL.DeleteFeedbackAsync(id,userId);
            if (result != "Ok") { return BadRequest(result); }
            return Ok(result);
        }
    }
}
