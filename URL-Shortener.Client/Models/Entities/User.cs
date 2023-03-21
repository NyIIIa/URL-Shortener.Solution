namespace URL_Shortener.Client.Models.Entities;

public class User
{
    public int Id { get; set; }
    public string? Login { get; set; }
    public string? PasswordHash { get; set; }
    public Role? Role { get; set; }
    public IEnumerable<ShortenedUrl>? ShortenedUrls { get; set; }
}