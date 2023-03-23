using URL_Shortener.Client.Models.Authentication;
using URL_Shortener.Client.Models.DTOs.Authentication;

namespace URL_Shortener.Client.Interfaces.Authentication;

public interface IAuthenticationService
{
    void Register(RegisterRequestDto registerRequest);
    AuthResult Login(LoginRequestDto loginRequest);
}