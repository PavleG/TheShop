﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.WebApi.Models;
using Shop.WebApi.Services;

namespace Shop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShopController : ControllerBase
{
    private readonly ISupplierService _supplierService;
    private readonly ILogger<ShopController> _logger;

    public ShopController(ISupplierService supplierService, ILogger<ShopController> logger)
    {
        _supplierService = supplierService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetArtice(int id, int maxExpectedPrice = 200, CancellationToken cancellationToken = default)
    {
        var article = await _supplierService.GetArticleAsync(id, maxExpectedPrice, cancellationToken);
        if (article == null)
        {
            return NotFound();
        }
        return Ok(article);
    }

    [HttpPost]
    public void BuyArticle(Article article, int buyerId)
    {
        _logger.LogDebug("Trying to sell article with id={id}.", article.Id);
        //_supplierService.BuyArticle(article, buyerId);
    }
}
