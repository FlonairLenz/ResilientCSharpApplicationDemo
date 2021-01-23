using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RetryController : ControllerBase
    {
        private readonly ILogger<RetryController> logger;
        private readonly IRetryService retryService;

        public RetryController(ILogger<RetryController> logger,
            IRetryService retryService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.retryService = retryService ??
                                throw new ArgumentNullException(nameof(retryService));
        }

        [HttpGet]
        public Task<string> GetString()
        {
            return this.retryService.GetString();
        }
    }
}