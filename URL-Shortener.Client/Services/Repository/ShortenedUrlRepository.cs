using URL_Shortener.Client.Data;
using URL_Shortener.Client.Interfaces.Repository;
using URL_Shortener.Client.Models.Entities;

namespace URL_Shortener.Client.Services.Repository;

public class ShortenedUrlRepository : IShortenedUrlRepository
{
    private readonly UrlShortenedDbContext _dbContext;

    public ShortenedUrlRepository(UrlShortenedDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Add(ShortenedUrl entity)
    {
        var isOriginalUrlExist = _dbContext.ShortenedUrls
            .Any(su => su.OriginalUrl == entity.OriginalUrl);
        
        if (isOriginalUrlExist)
       {
           throw new Exception("The original url already exists");
       }

       _dbContext.ShortenedUrls.Add(entity);
       _dbContext.SaveChanges();
    }

    public void Delete(ShortenedUrl shortenedUrl)
    {
        _dbContext.ShortenedUrls.Remove(shortenedUrl);
        _dbContext.SaveChanges();
    }

    public IEnumerable<ShortenedUrl> GetAll()
    {
        return _dbContext.ShortenedUrls.ToList();
    }
}