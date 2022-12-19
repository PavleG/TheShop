using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.WebApi.Enumerations;
using Shop.WebApi.Models;
using Shop.WebApi.Services;

namespace Shop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShopController : ControllerBase
{
    private readonly ISupplierService _supplierService;
    private readonly ISaleService _saleService;
    private readonly ILogger<ShopController> _logger;

    public ShopController(ISupplierService supplierService, ISaleService saleService, ILogger<ShopController> logger)
    {
        _supplierService = supplierService;
        _logger = logger;
        _saleService = saleService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetArtice(int id, int maxExpectedPrice = 200, CancellationToken cancellationToken = default)
    {
        try
        {
            var article = await _supplierService.GetArticleAsync(id, maxExpectedPrice, cancellationToken);
            return article == null ? NotFound() : Ok(article);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Problem();
        }
        
    }

    [HttpPost]
    public IActionResult BuyArticle(BuyRequestDto requestData)
    {
        _logger.LogInformation("Trying to sell article with id={id} to buyer with id={buyerId}.", 
            requestData.ArticleInfo.Id, requestData.BuyerId);
        var response = _saleService.SaleArticle(requestData.ArticleInfo, requestData.BuyerId);
        switch (response)
        {
            case SaleResponse.Success:
                _logger.LogInformation("Article with id {id} is sold.", requestData.ArticleInfo.Id);
                return NoContent();
            case SaleResponse.Error:
                _logger.LogError("Could not save article with id {id}. ", requestData.ArticleInfo.Id);
                return BadRequest();
            default:
                _logger.LogError($"{nameof(BuyArticle)} failiure.");
                return Problem();
        }
    }
}
