using Shop.WebApi.Models;

namespace Shop.WebApi.Services
{
    public interface IArticleRepository
    {
        bool ArticleInInventory(int id);
        Article GetArticle(int id);
    }
}