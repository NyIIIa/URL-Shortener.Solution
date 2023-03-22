using URL_Shortener.Client.Data;
using URL_Shortener.Client.Interfaces.Repository;

namespace URL_Shortener.Client.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    UrlShortenedDbContext UrlShortenedDbContext { get; }
    IUserRepository UserRepository { get; }
    IShortenedUrlRepository ShortenedUrlRepository { get; }
    void SaveChanges();
}