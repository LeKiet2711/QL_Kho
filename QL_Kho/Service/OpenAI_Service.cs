using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

public class OpenAI_Service
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenAI_Service(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenAI:ApiKey"];
    }

    public async Task<string> GetResponseFromOpenAI(string prompt)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        var requestBody = new
        {
            prompt = prompt,
            max_tokens = 100
        };

        var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }
}
