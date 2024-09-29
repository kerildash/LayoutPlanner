using System.Net;

namespace Services;

public class HttpService
{
    private readonly HttpClient _client;

    public HttpService()
    {
        HttpClientHandler handler = new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.All
        };

        _client = new HttpClient(handler);
    }

    public async Task<string> GetAsync(string uri)
    {
        HttpResponseMessage response = await _client.GetAsync(uri);

        return await response.Content.ReadAsStringAsync();
    }
}
