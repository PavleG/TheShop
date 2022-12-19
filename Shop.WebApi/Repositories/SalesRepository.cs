using Shop.WebApi.Models;

namespace Shop.WebApi.Repositories;

public class SalesRepository : ISalesRepository
{
    private readonly List<SoldArticle> _articles = new();

    //this is not used?
    public SoldArticle GetById(int id)
    {
        return _articles.Single(x => x.Id == id);
    }

    public void Save(SoldArticle article)
    {
        _articles.Add(article);
    }
}
