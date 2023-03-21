namespace URL_Shortener.Client.Models.Authentication;

public class JwtSettings
{
    public string? Secret { get; set; }
    public int ExpiryDays { get; set; }
}