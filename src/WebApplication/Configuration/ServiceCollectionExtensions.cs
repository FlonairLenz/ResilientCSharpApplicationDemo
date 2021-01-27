using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Services;

namespace WebApplication.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHttpClientFactoryWithPolicies(this IServiceCollection services, IConfiguration configuration)
        {
            // Use HttpClient Factory
            services.AddHttpClient(nameof(UserService), client =>
            {
                client.BaseAddress = new Uri(configuration["UserApiBaseAddress"]);
            });

            // Use timeout resilitent pattern
            services.AddHttpClient(nameof(CalculateLongRunningService),
                    client => client.BaseAddress = new Uri(configuration["LongRunningApiBaseAddress"]))
                .AddPolicyHandler(CalculateLongRunningService.GetPolicies());

            // Use timeout with fallback
            services.AddHttpClient(nameof(CalculateLongRunningWithFallbackService),
                    client => { client.BaseAddress = new Uri(configuration["LongRunningApiBaseAddress"]); })
                .AddPolicyHandler(CalculateLongRunningWithFallbackService.GetPolicies());

            services.AddHttpClient(nameof(RetryService),
                    client => client.BaseAddress = new Uri(configuration["LongRunningApiBaseAddress"]))
                .AddPolicyHandler(RetryService.GetPolicies());
        }

        public static void AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICalculateLongRunningService, CalculateLongRunningService>();
            services.AddScoped<ICalculateLongRunningWithFallbackService, CalculateLongRunningWithFallbackService>();
            services.AddScoped<IRetryService, RetryService>();
        }
    }
}