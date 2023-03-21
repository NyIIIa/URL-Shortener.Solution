using URL_Shortener.Client.Interfaces.Authentication;
using URL_Shortener.Client.Models.Authentication;
using URL_Shortener.Client.Models.DTOs.Authentication;

namespace URL_Shortener.Client.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenService _jwtTokenService;

    public AuthenticationService(IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }
    
    public AuthResult Register(RegisterRequestDto registerRequest)
    {
        throw new NotImplementedException();
    }

    public AuthResult Login(LoginRequestDto loginRequest)
    {
        throw new NotImplementedException();
    }
}