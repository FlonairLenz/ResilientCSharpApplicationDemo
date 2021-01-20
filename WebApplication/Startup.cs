using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication.Services;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Use HttpClient Factory
            services.AddHttpClient(nameof(UserService), client =>
            {
                client.BaseAddress = new Uri(this.Configuration["UserApiBaseAddress"]);
            });

            // Use timeout resilitent pattern
            services.AddHttpClient(nameof(CalculateLongRunningService),
                client => client.BaseAddress = new Uri(this.Configuration["LongRunningApiBaseAddress"]))
                .AddPolicyHandler(CalculateLongRunningService.GetPolicies());

            // Use timeout with fallback
            services.AddHttpClient(nameof(CalculateLongRunningWithFallbackService),
                client => { client.BaseAddress = new Uri(this.Configuration["LongRunningApiBaseAddress"]); })
                .AddPolicyHandler(CalculateLongRunningWithFallbackService.GetPolicies());

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICalculateLongRunningService, CalculateLongRunningService>();
            services.AddScoped<ICalculateLongRunningWithFallbackService, CalculateLongRunningWithFallbackService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}