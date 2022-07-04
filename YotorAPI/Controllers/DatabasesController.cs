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
    public class DatabasesController : ControllerBase
    {
        private Business_Logic_Layer.BLL.DatabaseBLL _databaseBLL;
        private Guid userId => Guid.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public DatabasesController()
        {
            _databaseBLL = new Business_Logic_Layer.BLL.DatabaseBLL();
        }
        [HttpGet]
        public async Task<IActionResult> CreateBackupAsync()
        {
            var result = await _databaseBLL.CreateBackupAsync(userId);
            if (result != "Ok") { return BadRequest(result); }
            return Ok(result);
        }
        [HttpGet("RestoreByLastBackup")]
        public async Task<IActionResult> RestoreDatabaseByLastBackupAsync()
        {
            var result = await _databaseBLL.RestoreDatabaseByLastBackupAsync();
            if (result != "Ok") { return BadRequest(result); }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> RestoreDatabaseBySomeBackupAsync(Guid id)
        {
            var result = await _databaseBLL.RestoreDatabaseBySomeBackupAsync(id);
            if (result != "Ok") { return BadRequest(result); }
            return Ok(result);
        }
    }
}
