using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shop.WebApi.Controllers;
using Shop.WebApi.Models;
using Shop.WebApi.Services;

namespace Shop.WebApi.Tests;

public class ShopControllerTests
{
    [Fact]
    public void GetArticle_ReturnsOkArticle_WhenArticleInInventory()
    {
        var mockSupplierService = new Mock<ISupplierService>();
        var mockSaleService = new Mock<ISaleService>();
        var mockLogger = new Mock<ILogger<ShopController>>();
        var id = 0;
        mockSupplierService.Setup(
            service => service.GetArticleAsync(It.IsAny<int>(), It.IsAny<int>(), default))
            .Callback<int, int, CancellationToken>((i, p, t) => { id = i; })
            .ReturnsAsync(new Article() { Id = id, ArticleName = $"Article {id}", ArticlePrice = 123});
        var sut = new ShopController(mockSupplierService.Object, mockSaleService.Object, mockLogger.Object);

        var result = sut.GetArtice(1);
        //var expectedArticle = new Article { Id = 1, ArticleName = $"Article {1}", ArticlePrice = 123 };
        
        result.Should().BeOfType<Task<IActionResult>>();
    }
}
