using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VoiceSocialNetworks.DataLayer.Models;

namespace VoiceSocialNetworks.SDK.Clients
{
    public class YandexClient : IYandexClient, IDisposable
    {
        private readonly HttpClient _httpClient;
        private const string USER_INFO_URL = "https://login.yandex.ru/info";
        public YandexClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<User> GetUser(string oauthToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, USER_INFO_URL);
            Console.WriteLine($"Request to Yandex GetUserInfo with header value = {oauthToken}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Invalid response to {nameof(GetUser)} with status code = {response.StatusCode}");
                return null;
            }

            var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            Console.WriteLine($"{nameof(GetUser)} returned user: {user}");

            return user;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
