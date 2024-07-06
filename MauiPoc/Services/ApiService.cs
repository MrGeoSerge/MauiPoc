using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MauiPoc.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = GetBaseAddress();
        }

        private Uri? GetBaseAddress()
        {
            var config = MauiProgram.GetService<IConfiguration>();
            var serverUrl = config.GetSection("ServerUrl")?.Value;
            return new Uri(serverUrl);

        }

        public async Task<string> SendDataAsync(string token)
        {
            try
            {
                var tokenJson = JsonConvert.SerializeObject(new { token });
                var content = new StringContent(tokenJson, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/token", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine($"Request error: {httpRequestException.Message}");
                return $"Request error: {httpRequestException.Message}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return $"Unexpected error: {ex.Message}";
            }
        }

    }
}
