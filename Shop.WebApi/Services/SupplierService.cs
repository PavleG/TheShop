using Shop.WebApi.Configurations;
using Shop.WebApi.Models;
using Shop.WebApi.Repositories;

namespace Shop.WebApi.Services;

public class SupplierService : ISupplierService
{
    private readonly IEnumerable<IArticleProvider> _articleProviders;

    public SupplierService(IEnumerable<IArticleProvider> articleProviders)
    {
        _articleProviders = articleProviders;
    }

    public async Task<Article?> GetArticleAsync(
        int id,
        int maxExpectedPrice = 200,
        CancellationToken cancellationToken = default)
    {
        Article? article = await FindArticleAsync<Warehouse>(id, maxExpectedPrice, cancellationToken)
            ?? await FindArticleAsync<Vendor<Vendor1Settings>>(id, maxExpectedPrice, cancellationToken)
            ?? await FindArticleAsync<Vendor<Vendor2Settings>>(id, maxExpectedPrice, cancellationToken);

        return article;
    }

    private async Task<Article?> FindArticleAsync<T>(
        int id, 
        int maxPrice, 
        CancellationToken cancellationToken)
    {
        Article? article = null;
        var provider = _articleProviders.First(ap => ap.GetType() == typeof(T));
        if (await provider.ArticleInInventoryAsync(id, cancellationToken))
        {
            article = await provider.GetArticleAsync(id, cancellationToken);
            if (article != null && article.ArticlePrice <= maxPrice)
            {
                return article;
            }
        }
        return article;
    }
}
