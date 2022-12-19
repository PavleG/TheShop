using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Shop.WebApi.Models;
using Shop.WebApi.Services;

namespace Shop.WebApi.Tests;

public class CachedSupplierTests
{
    [Fact]
    public void CachedSupplier_ShouldReturnArticle_WhenArticlePresentInCache()
    {
        IMemoryCache memoryCacheMock = new MemoryCache(new MemoryCacheOptions());
        Mock<ISupplierService> supplierServiceMock = new Mock<ISupplierService>();
        Article article = new Article() { Id = 23, ArticleName = "Article 23", ArticlePrice = 100};
        memoryCacheMock.Set("article-23", article);

        var sut = new CachedSupplier(memoryCacheMock, supplierServiceMock.Object);

        var result = sut.GetArticleAsync(23).Result;
        supplierServiceMock.Verify(mock => mock.GetArticleAsync(It.IsIn<int>(), It.IsAny<int>(), new CancellationToken()), Times.Never);
        result.Should().Be(article);
    }
    [Fact]
    public void CachedSupplier_ShouldCallSupplierService_WhenArticleNotInCache()
    {
        IMemoryCache memoryCacheMock = new MemoryCache(new MemoryCacheOptions());
        Mock<ISupplierService> supplierServiceMock = new Mock<ISupplierService>();

        var sut = new CachedSupplier(memoryCacheMock, supplierServiceMock.Object);

        var id = It.IsAny<int>();
        var result = sut.GetArticleAsync(id).Result;
        result.Should().BeNull();
        supplierServiceMock.Verify(mock => mock.GetArticleAsync(id, It.IsAny<int>(), new CancellationToken()), Times.Once);
    }
}
