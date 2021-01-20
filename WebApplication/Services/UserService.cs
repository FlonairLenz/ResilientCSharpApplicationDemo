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
        private readonly HttpClient httpClient;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory?.CreateClient(nameof(UserService)) ?? throw new ArgumentNullException(nameof(httpClient));
        }
        
        public async Task<UserModel> GetUserAsync()
        {
            var userResponseString = await this.httpClient.GetStringAsync("");
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