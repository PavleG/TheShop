using Vendor.WebApi.Models;
using Vendor.WebApi.Repositories;

namespace Vendor.WebApi.Services;

public class SupplierService : ISupplierService
{
    private readonly SupplierRepository _supplierRepository;

    public SupplierService(SupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public bool IsInInventory(int id)
    {
        return _supplierRepository.ArticleInInventory(id);
    }
    public Article? GetArtice(int id)
    {
        return _supplierRepository.ArticleInInventory(id) ? _supplierRepository.GetArticle(id) : null;
    }
}
