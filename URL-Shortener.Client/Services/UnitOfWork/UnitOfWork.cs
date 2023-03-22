using URL_Shortener.Client.Data;
using URL_Shortener.Client.Interfaces.Repository;
using URL_Shortener.Client.Interfaces.UnitOfWork;
using URL_Shortener.Client.Services.Repository;

namespace URL_Shortener.Client.Services.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public UrlShortenedDbContext UrlShortenedDbContext { get; }
    public IUserRepository UserRepository { get; }
    public IShortenedUrlRepository ShortenedUrlRepository { get; }

    public UnitOfWork(UrlShortenedDbContext dbContext)
    {
        UrlShortenedDbContext = dbContext;
        UserRepository = new UserRepository(dbContext);
        ShortenedUrlRepository = new ShortenedUrlRepository(dbContext);
    }
    
    public void SaveChanges()
    {
        UrlShortenedDbContext.SaveChanges();
    }
}