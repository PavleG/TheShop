using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shop.WebApi.Configurations;
using Shop.WebApi.Models;

namespace Shop.WebApi.Services;

public class Dealer1 : IArticleRepository
{
    private readonly string _supplierUrl;

    public Dealer1(IOptions<Dealer1Settings> settings)
    {
        _supplierUrl = settings.Value.Url;
    }

    public bool ArticleInInventory(int id)
    {
        using (var client = new HttpClient())
        {
            var response = client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{_supplierUrl}/ArticleInInventory/{id}"));
            var hasArticle = JsonConvert.DeserializeObject<bool>(response.Result.Content.ReadAsStringAsync().Result);

            return hasArticle;
        }
    }

    public Article GetArticle(int id)
    {
        using (var client = new HttpClient())
        {
            var response = client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{_supplierUrl}/ArticleInInventory/{id}"));
            var article = JsonConvert.DeserializeObject<Article>(response.Result.Content.ReadAsStringAsync().Result);

            return article;
        }
    }
}
