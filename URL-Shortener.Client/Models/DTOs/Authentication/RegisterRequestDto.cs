namespace URL_Shortener.Client.Models.DTOs.Authentication;

public class RegisterRequestDto
{
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
}