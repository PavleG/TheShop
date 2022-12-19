using Vendor.WebApi.Models;

namespace Vendor.WebApi.Services
{
    public interface ISupplierService
    {
        Article? GetArtice(int id);
        bool IsInInventory(int id);
    }
}