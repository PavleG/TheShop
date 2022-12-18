using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Vendor.WebApi.Models;
using Vendor.WebApi.Services;

namespace Vendor.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SupplierController : ControllerBase
{
    private readonly DatabaseDriver _databaseDriver;
    private readonly SupplierService _supplier;
    private readonly ILogger<SupplierController> _logger;

    public SupplierController(DatabaseDriver databaseDriver, SupplierService supplier, ILogger<SupplierController> logger)
    {
        _databaseDriver = databaseDriver;
        _supplier = supplier;
        _logger = logger;
    }
    [HttpGet("ArticleInInventory/{id}")]
    public bool ArticleInInventory(int id)
    {
        return _supplier.ArticleInInventory(id);
    }
    [HttpGet("{id}")]
    public Article GetArtice(int id)
    {
        var articleExists = _supplier.ArticleInInventory(id);
        if (articleExists)
        {
            return _supplier.GetArticle(id);
        }
        else
        {
            throw new Exception("Article does not exist.");
        }
    }
    [HttpPost]
    public void BuyArticle(Article article, int buyerId)
    {
        var id = article.ID;
        if (article == null)
        {
            throw new Exception("Could not order article");
        }

        _logger.LogDebug("Trying to sell article with id={id}", id);

        article.IsSold = true;
        article.SoldDate = DateTime.Now;
        article.BuyerUserId = buyerId;

        try
        {
            _databaseDriver.Save(article);
            _logger.LogInformation("Article with id={id} is sold.", id);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError("Could not save article with id={id}", id);
            throw new Exception("Could not save article with id");
        }
        catch (Exception)
        {
        }
    }
}
