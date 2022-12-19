namespace Shop.WebApi.Models;
public record Article
{
    public int Id { get; init; }
    public string ArticleName { get; init; }
    public int ArticlePrice { get; init; }
}
