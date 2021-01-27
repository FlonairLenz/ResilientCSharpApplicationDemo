using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HeavyLoadSystemApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LongRunningController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> GetResultAsync()
        {
            await Task.Delay(5200);
            return Ok("Done");
        }
        
        [HttpGet("retry")]
        public ActionResult GetResultRetry()
        {
            if (new Random().Next(100) % 2 == 0)
            {
                return Conflict("Unexpected error");
            }
            
            return Ok("Test");
        }
    }
}