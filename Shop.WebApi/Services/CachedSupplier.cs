using Shop.WebApi.Models;

namespace Shop.WebApi.Services;

public class CachedSupplier
{
    private Dictionary<int, Article> _cachedArticles = new Dictionary<int, Article>();
    public bool ArticleInInventory(int id)
    {
        return _cachedArticles.ContainsKey(id);
    }

    public Article GetArticle(int id)
    {
        _cachedArticles.TryGetValue(id, out Article article);
        return article;
    }

    public void SetArticle(Article article)
    {
        _cachedArticles.Add(article.ID, article);
    }
}
