using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Polly.Timeout;

namespace WebApplication.Services
{
    public class CalculateLongRunningWithFallbackService : ICalculateLongRunningWithFallbackService
    {
        private const int Timeout = 2;
        private readonly HttpClient httpClient;

        public CalculateLongRunningWithFallbackService(IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory?.CreateClient(nameof(CalculateLongRunningWithFallbackService)) ?? throw new ArgumentNullException(nameof(httpClient));
        }
        
        public Task<string> CalculateLongRunningApi()
        {
            return this.httpClient.GetStringAsync("");
        }

        public static IAsyncPolicy<HttpResponseMessage> GetPolicies()
            => Policy.WrapAsync(GetFallbackPolicy(), GetTimeoutExceptionPolicy());
        
        private static IAsyncPolicy<HttpResponseMessage> GetTimeoutExceptionPolicy()
            => Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(Timeout), TimeoutStrategy.Pessimistic,
                (context, span, task) => throw new TimeoutException($"{nameof(CalculateLongRunningService)} does not return any data."));

        private static IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
            => Policy<HttpResponseMessage>.Handle<TimeoutException>()
                .FallbackAsync(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Default Json")});
    }
}