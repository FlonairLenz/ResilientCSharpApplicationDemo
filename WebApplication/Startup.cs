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
            services.AddHttpClient<IUserService, UserService>(client =>
            {
                client.BaseAddress = new Uri(this.Configuration["UserApiBaseAddress"]);
            });

            // Use timeout resilitent pattern
            services.AddHttpClient<ICalculateLongRunningService, CalculateLongRunningService>(
                client => client.BaseAddress = new Uri(this.Configuration["LongRunningApiBaseAddress"]))
                .AddPolicyHandler(CalculateLongRunningService.GetPolicies());

            // Use timeout with fallback
            services.AddHttpClient<ICalculateLongRunningWithFallbackService, CalculateLongRunningWithFallbackService>(
                client => { client.BaseAddress = new Uri(this.Configuration["LongRunningApiBaseAddress"]); })
                .AddPolicyHandler(CalculateLongRunningWithFallbackService.GetPolicies());
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