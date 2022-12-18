using Shop.WebApi.Models;
using Shop.WebApi.Repositories;

namespace Shop.WebApi.Services;

public class SupplierService : ISupplierService
{
    private readonly Db _db;
    private readonly CachedSupplier _cachedSupplier;
    private readonly Warehouse _warehouse;
    private readonly Dealer1 _dealer1;
    private readonly Dealer2 _dealer2;
    public SupplierService(Db db, CachedSupplier cachedSupplier, Warehouse warehouse,
        Dealer1 dealer1, Dealer2 dealer2)
    {
        _db = db;
        _cachedSupplier = cachedSupplier;
        _warehouse = warehouse;
        _dealer1 = dealer1;
        _dealer2 = dealer2;
    }

    public async Task<Article> GetArticeAsync(int id, int maxExpectedPrice = 200)
    {
        Article article = null;
        Article tmp = null;
        var articleExists = _cachedSupplier.ArticleInInventory(id);
        if (articleExists)
        {
            tmp = _cachedSupplier.GetArticle(id);
            if (maxExpectedPrice < tmp.ArticlePrice)
            {
                articleExists = await _warehouse.ArticleInInventoryAsync(id);
                if (articleExists)
                {
                    tmp = await _warehouse.GetArticleAsync(id);
                    if (maxExpectedPrice < tmp.ArticlePrice)
                    {
                        articleExists = await _dealer1.ArticleInInventoryAsync(id);
                        if (articleExists)
                        {
                            tmp = await _dealer1.GetArticleAsync(id);
                            if (maxExpectedPrice < tmp.ArticlePrice)
                            {
                                articleExists = await _dealer2.ArticleInInventoryAsync(id);
                                if (articleExists)
                                {
                                    tmp = await _dealer2.GetArticleAsync(id);
                                    if (maxExpectedPrice < tmp.ArticlePrice)
                                    {
                                        article = tmp;
                                    }
                                }
                            }
                        }
                    }
                }
                if (article != null)
                {
                    _cachedSupplier.SetArticle(article);
                }
            }
        }

        return article;
    }

    public void BuyArticle(Article article, int buyerId)
    {
        var id = article.Id;
        if (article == null)
        {
            throw new Exception("Could not order article");
        }

        article.IsSold = true;
        article.SoldDate = DateTime.Now;
        article.BuyerUserId = buyerId;

        try
        {
            _db.Save(article);
            //_logger.LogInformation("Article with id {id} is sold.", id);
        }
        catch (ArgumentNullException ex)
        {
            //_logger.LogError("Could not save article with id={id}.", id);
            throw new Exception("Could not save article with id");
        }
        catch (Exception)
        {
        }
    }
}
