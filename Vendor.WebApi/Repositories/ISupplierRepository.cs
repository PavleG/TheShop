using Vendor.WebApi.Models;

namespace Vendor.WebApi.Repositories
{
    public interface ISupplierRepository
    {
        bool ArticleInInventory(int id);
        Article GetArticle(int id);
    }
}