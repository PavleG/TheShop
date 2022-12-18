using Shop.WebApi.Models;

namespace Shop.WebApi.Repositories;

public class Db
{
    private readonly List<Article> _articles = new();

    public Article GetById(int id)
    {
        return _articles.Single(x => x.ID == id);
    }

    public void Save(Article article)
    {
        _articles.Add(article);
    }
}
