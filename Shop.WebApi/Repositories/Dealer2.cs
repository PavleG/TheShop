﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shop.WebApi.Configurations;
using Shop.WebApi.Models;

namespace Shop.WebApi.Repositories;

public class Dealer2 : IArticleRepository
{
    private readonly HttpClient _httpClient;

    public Dealer2(HttpClient httpClient, IOptions<Dealer2Settings> settings)
    {
        _httpClient = httpClient;
        if (!string.IsNullOrEmpty(settings.Value.Url))
        {
            _httpClient.BaseAddress = new Uri(settings.Value.Url);
        }
        else
        {
            throw new ArgumentNullException(paramName: nameof(Dealer2Settings), message: $"Missing url base addess.");
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
