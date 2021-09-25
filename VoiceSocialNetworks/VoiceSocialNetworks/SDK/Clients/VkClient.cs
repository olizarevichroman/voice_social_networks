using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace VoiceSocialNetworks.SDK.Clients
{
    public class VkClient : IVkClient
    {
        private readonly string BASE_URL = "https://api.vk.com/method";
        private readonly string API_VERSION = "5.103";
        public async Task<string> GetStatus(string accessToken)
        {
            using var httpClient = new HttpClient();
            var parameters = new Dictionary<string, string>
            {
                { "v", API_VERSION },
                { "access_token", accessToken }
            };
            var queryBuilder = new QueryBuilder(parameters);
            var uriBuilder = new UriBuilder(BASE_URL)
            {
                Query = queryBuilder.ToQueryString().Value,
                Path = "method/status.get"
            };
            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
            var response = await httpClient.SendAsync(httpRequest);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Vkontakte status.get return an error with code {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(jsonResponse);
            var statusText = jsonDocument.RootElement.GetProperty("response").GetProperty("text").GetString();
            
            if (statusText == string.Empty)
            {
                statusText = "Ваш статус пуст";
            }

            return statusText;
        }

        public async Task<string> SetStatus(string accessToken, string status)
        {
            using var httpClient = new HttpClient();
            var parameters = new Dictionary<string, string>
            {
                { "v", API_VERSION },
                { "access_token", accessToken },
                { "text", status }
            };
            var queryBuilder = new QueryBuilder(parameters);
            var uriBuilder = new UriBuilder(BASE_URL)
            {
                Query = queryBuilder.ToQueryString().Value,
                Path = "method/status.set"
            };
            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
            var response = await httpClient.SendAsync(httpRequest);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Vkontakte status.set return an error with code {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(jsonResponse);
            var responseCode = jsonDocument.RootElement.GetProperty("response").GetInt32();
            if (responseCode == 1)
            {
                return "Статус успешно обновлён";
            }

            return "Произошла ошибка";
        }
    }
}
