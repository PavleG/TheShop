using Microsoft.Extensions.Options;
using Shop.WebApi.Configurations;
using Shop.WebApi.Models;
using System.Net;
using System.Text.Json;

namespace Shop.WebApi.Services;

public class VendorClient<TOptions> : IArticleProvider where TOptions : VendorSettings
{
    private readonly HttpClient _httpClient;
    private readonly VendorSettings _settings;

    public VendorClient(HttpClient httpClient, IOptions<TOptions> settings)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _httpClient.BaseAddress = new Uri(_settings.Url);
    }

    public async Task<bool> ArticleInInventoryAsync(int id, CancellationToken cancellationToken = default)
    {
        var relativeUri = $"{_settings.CheckArticleRoute.Trim('/')}/{id}";
        var requestUri = new Uri(_httpClient.BaseAddress!, relativeUri);
        var response = await _httpClient.GetAsync(requestUri, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<Article?> GetArticleAsync(int id, CancellationToken cancellationToken = default)
    {
        var relativeUri = $"{_settings.GetArticleRoute.Trim('/')}/{id}";
        var requestUri = new Uri(_httpClient.BaseAddress!, relativeUri);
        var httpResponse = await _httpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
        if (httpResponse.StatusCode == HttpStatusCode.OK)
        {
            var response = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var article = JsonSerializer.Deserialize<Article>(response, options);
            return article;
        }
        else if (httpResponse.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
        else
        {
            throw new HttpRequestException("Vendor issue");
        }
    }
}
