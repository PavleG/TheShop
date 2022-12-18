using Microsoft.Extensions.Options;
using Shop.WebApi.Configurations;
using Shop.WebApi.Models;

namespace Shop.WebApi.Repositories;

public class Vendor<TOptions> : IArticleRepository where TOptions : class, IVendorSettings
{
    private readonly HttpClient _httpClient;

    public Vendor(HttpClient httpClient, IOptions<TOptions> settings)
    {
        _httpClient = httpClient;
        if (!string.IsNullOrEmpty(settings.Value.Url))
        {
            _httpClient.BaseAddress = new Uri(settings.Value.Url);
        }
        else
        {
            throw new ArgumentNullException(paramName: nameof(TOptions), message: $"Missing url base addess.");
        }

    }

    public async Task<bool> ArticleInInventoryAsync(int id, CancellationToken cancellationToken = default)
    {
        var methodRoute = $"/ArticleInInventory/{id}";
        var response = await _httpClient.GetFromJsonAsync<bool>(_httpClient.BaseAddress + methodRoute);
        return response;
    }

    public async Task<Article?> GetArticleAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetFromJsonAsync<Article>(_httpClient.BaseAddress + $"/{id}");
        return response;
    }
}
