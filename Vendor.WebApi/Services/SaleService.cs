using Vendor.WebApi.Enumerations;
using Vendor.WebApi.Models;
using Vendor.WebApi.Repositories;

namespace Vendor.WebApi.Services;

public class SaleService : ISaleService
{
    private readonly ISalesRepository _salesRepository;

    public SaleService(ISalesRepository salesRepository)
    {
        _salesRepository = salesRepository;
    }

    public SaleResponse SaleArticle(Article article, int buyerId)
    {
        SoldArticle soldAricle = new()
        {
            Id = article.Id,
            ArticleName = article.ArticleName,
            ArticlePrice = article.ArticlePrice,
            IsSold = true,
            SoldDate = DateTime.Now,
            BuyerUserId = buyerId,
        };

        try
        {
            _salesRepository.Save(soldAricle);
            return SaleResponse.Success;
        }
        catch (Exception)
        {
            return SaleResponse.Error;
        }
    }
}
