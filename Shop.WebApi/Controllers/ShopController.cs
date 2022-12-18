using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.WebApi.Models;
using Shop.WebApi.Repositories;

namespace Shop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShopController : ControllerBase
{
    private readonly Db _db;
    private readonly CachedSupplier _cachedSupplier;
    private readonly Warehouse _warehouse;
    private readonly Dealer1 _dealer1;
    private readonly Dealer2 _dealer2;
    private readonly ILogger<ShopController> _logger;

    public ShopController(Db db, CachedSupplier cachedSupplier, Warehouse warehouse,
        Dealer1 dealer1, Dealer2 dealer2, ILogger<ShopController> logger)
    {
        _db = db;
        _cachedSupplier = cachedSupplier;
        _warehouse = warehouse;
        _dealer1 = dealer1;
        _dealer2 = dealer2;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public Article GetArtice(int id, int maxExpectedPrice = 200)
    {
        Article article = null;
        Article tmp = null;
        var articleExists = _cachedSupplier.ArticleInInventory(id);
        if (articleExists)
        {
            tmp = _cachedSupplier.GetArticle(id);
            if (maxExpectedPrice < tmp.ArticlePrice)
            {
                articleExists = _warehouse.ArticleInInventory(id);
                if (articleExists)
                {
                    tmp = _warehouse.GetArticle(id);
                    if (maxExpectedPrice < tmp.ArticlePrice)
                    {
                        articleExists = _dealer1.ArticleInInventory(id);
                        if (articleExists)
                        {
                            tmp = _dealer1.GetArticle(id);
                            if (maxExpectedPrice < tmp.ArticlePrice)
                            {
                                articleExists = _dealer2.ArticleInInventory(id);
                                if (articleExists)
                                {
                                    tmp = _dealer2.GetArticle(id);
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

    [HttpPost]
    public void BuyArticle(Article article, int buyerId)
    {
        var id = article.ID;
        if (article == null)
        {
            throw new Exception("Could not order article");
        }

        _logger.LogDebug("Trying to sell article with id={id}.",id);

        article.IsSold = true;
        article.SoldDate = DateTime.Now;
        article.BuyerUserId = buyerId;

        try
        {
            _db.Save(article);
            _logger.LogInformation("Article with id {id} is sold.", id);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError("Could not save article with id={id}.", id);
            throw new Exception("Could not save article with id");
        }
        catch (Exception)
        {
        }
    }
}
