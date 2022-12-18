using Shop.WebApi.Configurations;
using Shop.WebApi.Models;
using Shop.WebApi.Repositories;

namespace Shop.WebApi.Services;

public class SupplierService : ISupplierService
{
    private readonly Db _db;
    private readonly IEnumerable<IArticleProvider> _articleRepositories;

    public SupplierService(Db db, IEnumerable<IArticleProvider> articleRepositories)
    {
        _db = db;
        _articleRepositories = articleRepositories;
    }

    public async Task<Article> GetArticeAsync(int id, int maxExpectedPrice = 200)
    {
        Article article = null;
        Article tmp = null;
        foreach (var item in _articleRepositories)
        {
            var test = item is Vendor<Dealer1Settings>;
            if (test)
            {
                return await item.GetArticleAsync(id);
            }
        }
        //bool articleExists;
        //    if (maxExpectedPrice < tmp.ArticlePrice)
        //    {
        //        articleExists = await _warehouse.ArticleInInventoryAsync(id);
        //        if (articleExists)
        //        {
        //            tmp = await _warehouse.GetArticleAsync(id);
        //            if (maxExpectedPrice < tmp.ArticlePrice)
        //            {
        //                articleExists = await _dealer1.ArticleInInventoryAsync(id);
        //                if (articleExists)
        //                {
        //                    tmp = await _dealer1.GetArticleAsync(id);
        //                    if (maxExpectedPrice < tmp.ArticlePrice)
        //                    {
        //                        articleExists = await _dealer2.ArticleInInventoryAsync(id);
        //                        if (articleExists)
        //                        {
        //                            tmp = await _dealer2.GetArticleAsync(id);
        //                            if (maxExpectedPrice < tmp.ArticlePrice)
        //                            {
        //                                article = tmp;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

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
