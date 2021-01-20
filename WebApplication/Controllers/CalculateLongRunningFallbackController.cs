using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculateLongRunningFallbackController : ControllerBase
    {
        private readonly ILogger<CalculateLongRunningFallbackController> logger;
        private readonly ICalculateLongRunningWithFallbackService calculateLongRunningService;
        
        public CalculateLongRunningFallbackController(ILogger<CalculateLongRunningFallbackController> logger, ICalculateLongRunningWithFallbackService calculateLongRunningService)
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