using System.Threading.Tasks;

namespace WebApplication.Services
{
    public interface ICalculateLongRunningWithFallbackService
    {
        Task<string> CalculateLongRunningApi();
    }
}