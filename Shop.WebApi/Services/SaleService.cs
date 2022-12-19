using Shop.WebApi.Enumerations;
using Shop.WebApi.Models;
using Shop.WebApi.Repositories;

namespace Shop.WebApi.Services;

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
