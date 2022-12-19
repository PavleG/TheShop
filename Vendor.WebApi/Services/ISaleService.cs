using Vendor.WebApi.Enumerations;
using Vendor.WebApi.Models;

namespace Vendor.WebApi.Services
{
    public interface ISaleService
    {
        SaleResponse SaleArticle(Article article, int buyerId);
    }
}