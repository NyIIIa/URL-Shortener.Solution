using URL_Shortener.Client.Models.Entities;

namespace URL_Shortener.Client.Models.Authentication;

public class AuthResult
{
    public string? Token { get; set; }
    public User? User { get; set; }
}