using Microsoft.AspNetCore.Authorization;
using URL_Shortener.Client.Interfaces.Authentication;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener.Client.Models;
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
        try
        { 
            _authenticationService.Register(registerRequest);
            
            return RedirectToAction("Index", "Home");
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel {Message = e.Message});
        }
    }
    
    public IActionResult Login(LoginRequestDto loginRequest)
    {
        try
        {
            var authResult = _authenticationService.Login(loginRequest);
            var cookieOptions = new CookieOptions() {HttpOnly = true, SameSite = SameSiteMode.Strict};
            Response.Cookies.Append("X-Access-Token", authResult.Token, cookieOptions);
            Response.Cookies.Append("X-User-Login", authResult.User.Login, cookieOptions);
            Response.Cookies.Append("X-User-Role", authResult.User.Role.Name, cookieOptions);

            return RedirectToAction("Index", "Home");
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel {Message = e.Message});
        }
    }
    
    public IActionResult LoginView()
    {
        return View("Login");
    }
    
    public IActionResult RegisterView()
    {
        return View("Register");
    }
}