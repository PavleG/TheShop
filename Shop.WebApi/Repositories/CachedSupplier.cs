using Microsoft.Extensions.Caching.Memory;
using Shop.WebApi.Models;

namespace Shop.WebApi.Repositories;

public class CachedSupplier : IArticleProvider
{
    //private Dictionary<int, Article> _cachedArticles = new Dictionary<int, Article>();
    private readonly IMemoryCache _memoryCache;
    private readonly IArticleProvider _articleRepository;

    public CachedSupplier(IMemoryCache memoryCache, IArticleProvider articleRepository)
    {
        _memoryCache = memoryCache;
        _articleRepository = articleRepository;
    }

    public async Task<bool> ArticleInInventoryAsync(int id, CancellationToken cancellationToken = default)
    {
        string key = $"article-{id}";
        return await _memoryCache.GetOrCreateAsync( 
            key,
            async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
                return await _articleRepository.ArticleInInventoryAsync(id, cancellationToken);
            });
    }

    public async Task<Article?> GetArticleAsync(int id, CancellationToken cancellationToken = default)
    {
        string key = $"article-{id}";
        return await _memoryCache.GetOrCreateAsync(
            key,
            async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromHours(2));
                return await _articleRepository.GetArticleAsync(id, cancellationToken);
            });
    }
}
