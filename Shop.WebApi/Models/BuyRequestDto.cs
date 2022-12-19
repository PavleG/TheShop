using System.ComponentModel.DataAnnotations;

namespace Shop.WebApi.Models;

public record BuyRequestDto
{
    [Required(ErrorMessage = "Buyer ID is missing.")]
    public int BuyerId { get; init; }
    [Required(ErrorMessage = "Information about the article is missing.")]
    public Article ArticleInfo { get; init; }
}
