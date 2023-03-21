using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener.Client.Models.DTOs.Authentication;

namespace URL_Shortener.Client.Controllers;

public class AuthController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public IActionResult Register(RegisterRequestDto registerRequest)
    {
        throw new NotImplementedException();
    }
    
    public IActionResult Login(LoginRequestDto loginRequest)
    {
        throw new NotImplementedException();
    }
    
    public IActionResult LoginView()
    {
        throw new NotImplementedException();
    }
    
    public IActionResult RegisterView()
    {
        throw new NotImplementedException();
    }
}