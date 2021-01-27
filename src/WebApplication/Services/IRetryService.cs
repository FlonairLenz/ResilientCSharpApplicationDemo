using System.Threading.Tasks;

namespace WebApplication.Services
{
    public interface IRetryService
    {
        Task<string> GetString();
    }
}