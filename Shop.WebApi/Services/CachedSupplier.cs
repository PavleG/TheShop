using Microsoft.Extensions.Caching.Memory;
using Shop.WebApi.Models;
using Shop.WebApi.Repositories;

namespace Shop.WebApi.Services;

public class CachedSupplier : ISupplierService
{
    //private Dictionary<int, Article> _cachedArticles = new Dictionary<int, Article>();
    private readonly IMemoryCache _memoryCache;
    private readonly ISupplierService _supplierService;

    public CachedSupplier(IMemoryCache memoryCache, ISupplierService supplierService)
    {
        _memoryCache = memoryCache;
        _supplierService = supplierService;
    }

    public async Task<Article?> GetArticleAsync(
        int id,
        int maxExpectedPrice = 200,
        CancellationToken cancellationToken = default)
    {
        string key = $"article-{id}";
        return await _memoryCache.GetOrCreateAsync(
            key,
            async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromHours(2));
                return await _supplierService.GetArticleAsync(id, maxExpectedPrice, cancellationToken);
            });
    }
}
