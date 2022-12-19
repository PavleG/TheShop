using Microsoft.Extensions.Options;
using Shop.WebApi.Configurations;
using Shop.WebApi.Models;

namespace Shop.WebApi.Repositories;

public class Vendor<TOptions> : IArticleProvider where TOptions : VendorSettings
{
    private readonly HttpClient _httpClient;
    private readonly VendorSettings _settings;

    public Vendor(HttpClient httpClient, IOptions<TOptions> settings)
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
        var response = await _httpClient.GetFromJsonAsync<Article>(requestUri, cancellationToken);
        return response;
    }
}
