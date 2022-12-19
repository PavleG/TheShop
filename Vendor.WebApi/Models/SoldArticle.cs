namespace Vendor.WebApi.Models;
public record SoldArticle : Article
{
    public bool IsSold { get; set; }
    public DateTime SoldDate { get; set; }
    public int BuyerUserId { get; set; }
}
