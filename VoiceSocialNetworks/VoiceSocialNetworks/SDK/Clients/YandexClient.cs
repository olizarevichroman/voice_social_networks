using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
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

        //private async Task<User> GetUser(string oauthToken)
        //{
        //    using var request = new HttpRequestMessage(HttpMethod.Get, USER_INFO_URL);
        //    Console.WriteLine($"Request to Yandex GetUserInfo with header value = {oauthToken}");
        //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);
        //    var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        Console.WriteLine($"Invalid response to {nameof(GetUser)} with status code = {response.StatusCode}");
        //        return null;
        //    }

        //    var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
        //    Console.WriteLine($"{nameof(GetUser)} returned user: {JsonConvert.SerializeObject(user)}");

        //    return user;
        //}

        private async Task<string> GetUser(string oauthToken)
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

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<IEnumerable<Claim>> GetUserClaims(string oauthToken)
        {
            var jsonUser = await GetUser(oauthToken);
            if (jsonUser == null)
            {
                return Array.Empty<Claim>();
            }

            var yandexUser = JsonConvert.DeserializeObject<User>(jsonUser);
            var jsonRoot = JsonDocument.Parse(jsonUser).RootElement;
            var claims = jsonRoot.EnumerateObject()
                                    .Where(prop => prop.Value.ValueKind != JsonValueKind.Array)
                                    .Select(prop => new Claim(prop.Name, prop.Value.GetString()));

            Console.WriteLine($"In {nameof(GetUserClaims)} return claims: claims");

            return claims;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
