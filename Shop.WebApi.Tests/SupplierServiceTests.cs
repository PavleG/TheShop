using FluentAssertions;
using Moq;
using Shop.WebApi.Models;
using Shop.WebApi.Repositories;
using Shop.WebApi.Services;

namespace Shop.WebApi.Tests;

public class SupplierServiceTests
{
    // SupplierService depends on the concrete implementations
    // of IArticleProvider and it's not straightforward to mock
    // their behavior...

    //[Fact]
    //public void SomeTest()
    //{
    //    Mock<IArticleProvider> mockWarehouse = new Mock<IArticleProvider>();
    //    Mock<IArticleProvider> mockVendor1 = new Mock<IArticleProvider>();
    //    Mock<IArticleProvider> mockVendor2 = new Mock<IArticleProvider>();
    //    var article = new Article()
    //    {
    //        Id = 10,
    //        ArticleName = "Article 10",
    //        ArticlePrice = 100
    //    };
    //    mockWarehouse.Setup(mock => mock.ArticleInInventoryAsync(10, new CancellationToken()).Result).Returns(true);
    //    mockWarehouse.Setup(mock => mock.GetArticleAsync(10, new CancellationToken()).Result).Returns(article);
    //    IEnumerable<IArticleProvider> articleProviders = new List<IArticleProvider>()
    //    {
    //        mockWarehouse.Object,
    //        mockVendor1.Object,
    //        mockVendor2.Object,
    //    };

    //    var sut = new SupplierService(articleProviders);

    //    var result = sut.GetArticleAsync(10, int.MaxValue).Result;

    //    result.Should().Be(article);
    //}
}
