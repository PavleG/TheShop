using Shop.WebApi.Models;

namespace Shop.WebApi.Services;

public interface ISupplierService
{
    Task<Article?> GetArticleAsync(
        int id, 
        int maxExpectedPrice = 200, 
        CancellationToken cancellationToken = default);
}