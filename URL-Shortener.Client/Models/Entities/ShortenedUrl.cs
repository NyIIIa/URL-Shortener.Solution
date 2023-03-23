namespace URL_Shortener.Client.Models.Entities;

public class ShortenedUrl
{
    public int Id { get; set; }
    public string? OriginalUrl { get; set; }
    public string? ShortUrl { get; set; }
    public User? User { get; set; } // Created by
    public DateTime CreatedDate { get; set; }
}