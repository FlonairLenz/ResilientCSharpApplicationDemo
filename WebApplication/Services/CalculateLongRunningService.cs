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
        private readonly HttpClient httpClient;

        public CalculateLongRunningService(IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient(nameof(CalculateLongRunningService)) ?? throw new ArgumentNullException(nameof(httpClient));
        }
        
        public Task<string> CalculateLongRunningApi()
        {
            return this.httpClient.GetStringAsync("");
        }
        
        public static IAsyncPolicy<HttpResponseMessage> GetPolicies()
            => Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(Timeout), TimeoutStrategy.Pessimistic,
                (context, span, task) => throw new TimeoutException($"{nameof(CalculateLongRunningService)} does not return any data."));
    }
}