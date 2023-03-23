using Microsoft.EntityFrameworkCore;
using URL_Shortener.Client.Models.Entities;

namespace URL_Shortener.Client.Data;

public class UrlShortenedDbContext : DbContext
{
    public UrlShortenedDbContext(DbContextOptions<UrlShortenedDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
    public DbSet<UrlShortenerAlgorithm> UrlShortAlgorithms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortenedUrl>()
                    .HasOne(su => su.User)
                    .WithMany(u => u.ShortenedUrls);
        
        modelBuilder.Entity<User>()
                    .HasOne(u => u.Role)
                    .WithMany(r => r.Users);
        
        modelBuilder.Entity<Role>()
                    .HasData(new List<Role>()
        {
            new Role(){Id = 1, Name = "Admin"},
            new Role(){Id = 2, Name = "User"},
        });

        modelBuilder.Entity<UrlShortenerAlgorithm>()
            .HasData(new UrlShortenerAlgorithm()
            {
                Id = 1,
                Description = ""
            });
    }
}