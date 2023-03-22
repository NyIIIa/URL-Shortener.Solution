namespace URL_Shortener.Client.Models.DTOs.ShortUrl;

public class ShortUrlResponseDto
{
    public int Id { get; set; }
    public string? OriginalUrl { get; set; }
    public string? ShortUrl { get; set; }
}