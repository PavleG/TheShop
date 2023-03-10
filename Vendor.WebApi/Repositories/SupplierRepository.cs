using Vendor.WebApi.Models;

namespace Vendor.WebApi.Repositories;
public class SupplierRepository : ISupplierRepository
{
    public bool ArticleInInventory(int id)
    {
        return new Random().NextDouble() >= 0.5;
    }

    public Article GetArticle(int id)
    {
        return new Article()
        {
            Id = id,
            ArticleName = $"Article {id}",
            ArticlePrice = new Random().Next(100, 500)
        };
    }
}
