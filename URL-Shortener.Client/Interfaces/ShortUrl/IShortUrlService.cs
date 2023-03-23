namespace URL_Shortener.Client.Interfaces.ShortUrl;

public interface IShortUrlService
{
    string ShortUrl(string longUrl);
}