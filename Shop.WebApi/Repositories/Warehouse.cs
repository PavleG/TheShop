using Shop.WebApi.Models;

namespace Shop.WebApi.Repositories;

public class Warehouse : IArticleProvider
{
    public async Task<bool> ArticleInInventoryAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => (new Random().NextDouble() >= 0.5));   
    }

    public async Task<Article?> GetArticleAsync(int id, CancellationToken cancellationToken = default)
    {
        return new Article()
        {
            Id = id,
            ArticleName = $"Article {id}",
            ArticlePrice = new Random().Next(100, 500)
        };
    }
}
