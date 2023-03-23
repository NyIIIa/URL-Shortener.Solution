namespace URL_Shortener.Client.Models.Entities;

public class User
{
    public int Id { get; set; }
    public string? Login { get; set; }
    public byte[] PasswordHash { get; set; } = new byte[32];
    public byte[] PasswordSalt { get; set; } = new byte[32];
    public Role? Role { get; set; }
    public IEnumerable<ShortenedUrl>? ShortenedUrls { get; set; }
}