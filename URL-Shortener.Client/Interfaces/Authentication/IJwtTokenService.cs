namespace URL_Shortener.Client.Interfaces.Authentication;

public interface IJwtTokenService
{
    string GenerateToken(string login, string role);
}