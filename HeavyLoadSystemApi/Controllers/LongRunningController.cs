using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HeavyLoadSystemApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LongRunningController : ControllerBase
    {
        [HttpGet]
        public async Task<string> GetResultAsync()
        {
            await Task.Delay(5200);
            return await Task.FromResult("Done");
        }
    }
}