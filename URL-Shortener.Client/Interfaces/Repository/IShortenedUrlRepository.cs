using URL_Shortener.Client.Models.Entities;

namespace URL_Shortener.Client.Interfaces.Repository;

public interface IShortenedUrlRepository : IRepository<ShortenedUrl>
{
    void Delete(int idUrl);
    ShortenedUrl GetById(int idUrl);
    ShortenedUrl GetByOriginalUrl(string originalUrl);
    IEnumerable<ShortenedUrl> GetAll();
}