using URL_Shortener.Client.Models.Entities;

namespace URL_Shortener.Client.Interfaces.Repository;

public interface IShortenedUrlRepository : IRepository<ShortenedUrl>
{
    void Delete(ShortenedUrl shortenedUrl);
    IEnumerable<ShortenedUrl> GetAll();
}