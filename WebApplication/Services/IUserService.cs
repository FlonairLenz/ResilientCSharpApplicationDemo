using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IUserService
    {
        public Task<UserModel> GetUserAsync();
    }
}