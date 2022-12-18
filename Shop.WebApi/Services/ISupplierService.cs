using Shop.WebApi.Models;

namespace Shop.WebApi.Services;

public interface ISupplierService
{
    void BuyArticle(Article article, int buyerId);
    Task<Article> GetArticeAsync(int id, int maxExpectedPrice = 200);
}