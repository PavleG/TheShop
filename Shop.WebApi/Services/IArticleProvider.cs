using Shop.WebApi.Models;

namespace Shop.WebApi.Services
{
    public interface IArticleProvider
    {
        Task<bool> ArticleInInventoryAsync(int id, CancellationToken cancellationToken = default);
        Task<Article?> GetArticleAsync(int id, CancellationToken cancellationToken = default);
    }
}