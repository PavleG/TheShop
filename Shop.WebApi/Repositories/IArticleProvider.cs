using Shop.WebApi.Models;

namespace Shop.WebApi.Repositories
{
    public interface IArticleRepository
    {
        Task<bool> ArticleInInventoryAsync(int id, CancellationToken cancellationToken = default);
        Task<Article?> GetArticleAsync(int id, CancellationToken cancellationToken = default);
    }
}