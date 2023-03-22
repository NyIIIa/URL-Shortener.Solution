using Microsoft.EntityFrameworkCore;
using URL_Shortener.Client.Data;
using URL_Shortener.Client.Interfaces.Repository;
using URL_Shortener.Client.Models.Entities;

namespace URL_Shortener.Client.Services.Repository;

public class UserRepository : IUserRepository
{
    private readonly UrlShortenedDbContext _dbContext;

    public UserRepository(UrlShortenedDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Add(User entity)
    {
        var isUserExist = _dbContext.Users.Any(u => u.Login == entity.Login);
        if (isUserExist)
        {
            throw new Exception("The user with such login already exists");
        }

        _dbContext.Users.Add(entity);
        _dbContext.SaveChanges();
    }

    public bool IsUserExist(string login)
    {
        return _dbContext.Users.Any(u => u.Login == login);
    }

    public User GetUserByLogin(string login)
    {
        return _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Login == login);
    }
}