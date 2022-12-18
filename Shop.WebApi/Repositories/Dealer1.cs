using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shop.WebApi.Configurations;
using Shop.WebApi.Models;

namespace Shop.WebApi.Repositories;

public class Dealer1 : IArticleRepository
{
    private readonly HttpClient _httpClient;

    public Dealer1(HttpClient httpClient, IOptions<Dealer1Settings> settings)
    {
        _httpClient = httpClient;
        if (!string.IsNullOrEmpty(settings.Value.Url))
        {
            _httpClient.BaseAddress = new Uri(settings.Value.Url);
        }
        else
        {
            throw new ArgumentNullException(paramName: nameof(Dealer1Settings), message:$"Missing url base addess.");
        }

    }

    public async Task<bool> ArticleInInventoryAsync(int id)
    {
        var methodRoute = $"/ArticleInInventory/{id}";
        var response = await _httpClient.GetFromJsonAsync<bool>(_httpClient.BaseAddress + methodRoute);
        return response;
    }

    public async Task<Article?> GetArticleAsync(int id)
    {
        var response = await _httpClient.GetFromJsonAsync<Article>(_httpClient.BaseAddress + $"/{id}");
        return response;
    }
}
