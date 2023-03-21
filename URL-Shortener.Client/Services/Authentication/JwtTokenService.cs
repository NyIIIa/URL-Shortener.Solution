using Microsoft.Extensions.Options;
using URL_Shortener.Client.Interfaces.Authentication;
using URL_Shortener.Client.Models.Authentication;

namespace URL_Shortener.Client.Services.Authentication;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenService(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }
    
    public string GenerateToken(string login, string role)
    {
        throw new NotImplementedException();
    }
}