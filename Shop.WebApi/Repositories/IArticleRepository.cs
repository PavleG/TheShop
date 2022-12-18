using Shop.WebApi.Models;

namespace Shop.WebApi.Repositories
{
    public interface IArticleRepository
    {
        Task<bool> ArticleInInventoryAsync(int id);
        Task<Article?> GetArticleAsync(int id);
    }
}