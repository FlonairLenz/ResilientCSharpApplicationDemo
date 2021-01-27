using System;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;

namespace WebApplication.Services
{
    public class RetryService : IRetryService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RetryService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public Task<string> GetString()
        {
            return this.httpClientFactory.CreateClient(nameof(RetryService)).GetStringAsync("retry");
        }

        public static IAsyncPolicy<HttpResponseMessage> GetPolicies()
            => Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(
                    3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (ex, ts, context) =>
                    {
                        // Some logic on retry (logging, ...)
                        Console.WriteLine("Retry Logging");
                    });
    }
}