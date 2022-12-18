using Shop.WebApi.Models;
using Shop.WebApi.Repositories;

namespace Shop.WebApi.Services;

public class SaleService
{
    private readonly Db _db;

    public SaleService(Db db)
    {
        _db = db;
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
