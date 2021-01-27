using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculateLongRunningController : ControllerBase
    {
        private readonly ILogger<CalculateLongRunningController> logger;
        private readonly ICalculateLongRunningService calculateLongRunningService;
        
        public CalculateLongRunningController(ILogger<CalculateLongRunningController> logger, ICalculateLongRunningService calculateLongRunningService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.calculateLongRunningService = calculateLongRunningService ?? throw new ArgumentNullException(nameof(calculateLongRunningService));
        }

        [HttpGet]
        public Task<string> GetString()
        {
            return this.calculateLongRunningService.CalculateLongRunningApi();
        }
    }
}