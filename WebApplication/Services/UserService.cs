using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
        
        public async Task<UserModel> GetUserAsync()
        {
            using var httpClient = this.httpClientFactory.CreateClient(nameof(UserService));
            var userResponseString = await httpClient.GetStringAsync("");
            var userJson = JsonConvert.DeserializeObject<JObject>(userResponseString);
            var user = userJson["results"]?[0]?["name"]?.ToObject<UserModel>();

            if (user == null)
            {
                throw new Exception("Error while calling user");
            }

            return user;
        }
    }
}