using Shop.WebApi.Enumerations;
using Shop.WebApi.Models;

namespace Shop.WebApi.Services;

public interface ISaleService
{
    SaleResponse SaleArticle(Article article, int buyerId);
}