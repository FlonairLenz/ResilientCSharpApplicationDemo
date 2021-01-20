using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public class CalculateLongRunningService : ICalculateLongRunningService
    {
        private readonly HttpClient httpClient;

        public CalculateLongRunningService(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        
        public Task<string> CalculateLongRunningApi()
        {
            return this.httpClient.GetStringAsync("");
        }
    }
}