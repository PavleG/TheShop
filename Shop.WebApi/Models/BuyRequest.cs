using System.ComponentModel.DataAnnotations;

namespace Shop.WebApi.Models;

public record BuyRequest
{
    [Required(ErrorMessage = "Buyer ID is missing.")]
    public int BuyerId { get; init; }
    [Required(ErrorMessage = "Information about the article is missing.")]
    public Article ArticleInfo { get; init; }
}
