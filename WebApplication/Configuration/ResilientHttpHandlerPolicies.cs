using System;
using System.Net.Http;
using Polly;
using Polly.Timeout;

namespace WebApplication.Configuration
{
    public static class ResilientHttpHandlerPolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy(string timeoutErrorMessage, int timeout = 2)
            => Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(timeout), TimeoutStrategy.Pessimistic,
                (context, span, task) => throw new TimeoutException(timeoutErrorMessage));
    }
}