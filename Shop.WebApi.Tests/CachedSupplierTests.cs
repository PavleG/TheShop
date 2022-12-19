using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Shop.WebApi.Models;
using Shop.WebApi.Services;

namespace Shop.WebApi.Tests;

public class CachedSupplierTests
{
    [Fact]
    public void CachedSupplier_ShouldReturnArticleFromCache_WhenArticlePresentInCache()
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
    public void CachedSupplier_ShouldReturnArticleFromService_WhenArticleNotInCache()
    {
        IMemoryCache memoryCacheMock = new MemoryCache(new MemoryCacheOptions());
        Mock<ISupplierService> supplierServiceMock = new Mock<ISupplierService>();
        var id = It.IsAny<int>();
        Article article = new Article() { Id = id, ArticleName = $"Article {id}", ArticlePrice = 100 };
        supplierServiceMock.Setup<Article>(mock => mock.GetArticleAsync(id, 200, new CancellationToken()).Result).Returns(article);

        var sut = new CachedSupplier(memoryCacheMock, supplierServiceMock.Object);

        var result = sut.GetArticleAsync(id).Result;
        result.Should().Be(article);
        supplierServiceMock.Verify(mock => mock.GetArticleAsync(id, It.IsAny<int>(), new CancellationToken()), Times.Once);
    }

    [Fact]
    public void CachedSupplier_ShouldSaveArticleToCache_WhenArticleNotInCache()
    {
        IMemoryCache memoryCacheMock = new MemoryCache(new MemoryCacheOptions());
        Mock<ISupplierService> supplierServiceMock = new Mock<ISupplierService>();
        var id = It.IsAny<int>();
        Article article = new Article() { Id = id, ArticleName = $"Article {id}", ArticlePrice = 100 };
        supplierServiceMock.Setup<Article>(mock => mock.GetArticleAsync(id, 200, new CancellationToken()).Result).Returns(article);

        var sut = new CachedSupplier(memoryCacheMock, supplierServiceMock.Object);

        var result = sut.GetArticleAsync(id).Result;

        supplierServiceMock.Verify(mock => mock.GetArticleAsync(id, 200, new CancellationToken()), Times.Once);
        memoryCacheMock.Get($"article-{id}").Should().Be(article);
    }
}
