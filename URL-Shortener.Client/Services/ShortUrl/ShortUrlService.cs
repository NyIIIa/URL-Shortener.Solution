using URL_Shortener.Client.Interfaces.ShortUrl;

namespace URL_Shortener.Client.Services.ShortUrl;

public class ShortUrlService : IShortUrlService
{
    public string ShortUrl(string longUrl)
    {
        //Validate the input url
        if (!Uri.TryCreate(longUrl, UriKind.Absolute, out var inputUrl))
        {
            throw new Exception("Invalid url has been provided");
        }
        
        //Create a short version of the input url
        var random = new Random();
        const string symbols = "HqJ01z53b7ISOpOVoQhE";
        var randomStr = new string(Enumerable.Repeat(symbols, 8)
            .Select(x => x[random.Next(x.Length)]).ToArray());
        var shortUrl = $"{inputUrl.Host}/{randomStr}";
        return shortUrl;
    }
}