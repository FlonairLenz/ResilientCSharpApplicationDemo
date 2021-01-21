using System;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Polly.Timeout;

namespace WebApplication.Services
{
    public class CalculateLongRunningService : ICalculateLongRunningService
    {
        private const int Timeout = 2;
        private readonly IHttpClientFactory httpClientFactory;

        public CalculateLongRunningService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
        
        public Task<string> CalculateLongRunningApi()
        {
            return this.httpClientFactory.CreateClient(nameof(CalculateLongRunningService)).GetStringAsync("");
        }
        
        public static IAsyncPolicy<HttpResponseMessage> GetPolicies()
            => Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(Timeout), TimeoutStrategy.Pessimistic,
                (context, span, task) => throw new TimeoutException($"{nameof(CalculateLongRunningService)} does not return any data."));
    }
}