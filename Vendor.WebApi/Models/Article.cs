namespace Vendor.WebApi.Models;
public record Article
{
    public int Id { get; set; }
    public string ArticleName { get; set; }
    public int ArticlePrice { get; set; }
}
