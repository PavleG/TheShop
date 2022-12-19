using System.ComponentModel.DataAnnotations;

namespace Shop.WebApi.Configurations;

public class VendorSettings
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Missing property")]
    public string Url { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Missing property")]
    public string CheckArticleRoute { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Missing property")]
    public string GetArticleRoute { get; set; }
}
