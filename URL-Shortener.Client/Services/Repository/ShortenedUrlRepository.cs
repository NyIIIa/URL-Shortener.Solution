using Microsoft.EntityFrameworkCore;
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
    }

    public void Delete(int idUrl)
    {
       var shortenedUrl = _dbContext.ShortenedUrls.FirstOrDefault(su => su.Id == idUrl);
       if (shortenedUrl == null)
       {
           throw new Exception("The specified original url doesn't exist!");
       }
        _dbContext.ShortenedUrls.Remove(shortenedUrl);
    }

    public ShortenedUrl GetById(int idUrl)
    {
        return _dbContext.ShortenedUrls
            .Include(su => su.User)
            .Include(su => su.User.Role)
            .FirstOrDefault(su => su.Id == idUrl);
    }

    public ShortenedUrl GetByOriginalUrl(string originalUrl)
    {
        return _dbContext.ShortenedUrls
            .Include(su => su.User)
            .Include(su => su.User.Role)
            .FirstOrDefault(su => su.OriginalUrl == originalUrl);
    }

    public IEnumerable<ShortenedUrl> GetAll()
    {
        return _dbContext.ShortenedUrls.ToList();
    }
}