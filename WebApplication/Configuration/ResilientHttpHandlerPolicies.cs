using System;
using System.Net.Http;
using Polly;

namespace WebApplication.Configuration
{
    public static class ResilientHttpHandlerPolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy(int timeout = 2)
            => Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(timeout));
    }
}