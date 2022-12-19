using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Vendor.WebApi.Models;
using Vendor.WebApi.Services;
using Vendor.WebApi.Enumerations;

namespace Vendor.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SupplierController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly ISupplierService _supplierService;
    private readonly ILogger<SupplierController> _logger;

    public SupplierController(ISaleService saleService, ISupplierService supplier, ILogger<SupplierController> logger)
    {
        _saleService = saleService;
        _supplierService = supplier;
        _logger = logger;
    }
    [HttpGet("ArticleInInventory/{id:int}")]
    public bool ArticleInInventory(int id)
    {
        return _supplierService.IsInInventory(id);
    }
    [HttpGet("{id:int}")]
    public IActionResult GetArtice(int id)
    {
        var article = _supplierService.GetArtice(id);
        if (article != null)
        {
            return Ok(article);
        }
        return NotFound();
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
