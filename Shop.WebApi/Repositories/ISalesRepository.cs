using Shop.WebApi.Models;

namespace Shop.WebApi.Repositories
{
    public interface ISalesRepository
    {
        SoldArticle GetById(int id);
        void Save(SoldArticle article);
    }
}