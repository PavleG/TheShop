using Vendor.WebApi.Models;

namespace Vendor.WebApi.Repositories
{
    public interface ISalesRepository
    {
        Article GetById(int id);
        void Save(Article article);
    }
}