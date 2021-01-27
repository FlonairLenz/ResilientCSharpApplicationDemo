using System.Threading.Tasks;

namespace WebApplication.Services
{
    public interface ICalculateLongRunningService
    {
        public Task<string> CalculateLongRunningApi();
    }
}