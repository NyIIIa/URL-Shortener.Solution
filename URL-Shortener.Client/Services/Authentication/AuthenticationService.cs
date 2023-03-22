using System.Security.Cryptography;
using System.Text;
using URL_Shortener.Client.Interfaces.Authentication;
using URL_Shortener.Client.Interfaces.UnitOfWork;
using URL_Shortener.Client.Models.Authentication;
using URL_Shortener.Client.Models.DTOs.Authentication;
using URL_Shortener.Client.Models.Entities;

namespace URL_Shortener.Client.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthenticationService(IJwtTokenService jwtTokenService, IUnitOfWork unitOfWork)
    {
        _jwtTokenService = jwtTokenService;
        _unitOfWork = unitOfWork;
    }
    
    public void Register(RegisterRequestDto registerRequest)
    {
        var isUserExist = _unitOfWork.UserRepository.IsUserExist(registerRequest.Login);
        if (isUserExist)
        {
            throw new Exception("The user with such login already exists");
        }

        var roleFromDb = _unitOfWork.UrlShortenedDbContext.Roles
            .FirstOrDefault(r => r.Name == registerRequest.Role);
        if (roleFromDb == null)
        {
            throw new Exception("The role with such name doesn't exist");
        }
        
        GeneratePasswordHash(registerRequest.Password, 
                            out byte[] passwordHash, out byte[] passwordSalt);
        
        var user = new User()
        {
            Login = registerRequest.Login,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = roleFromDb
        };
        
        _unitOfWork.UserRepository.Add(user);
        _unitOfWork.SaveChanges();
    }

    public AuthResult Login(LoginRequestDto loginRequest)
    {
        var userFromDb = _unitOfWork.UserRepository.GetUserByLogin(loginRequest.Login);
        if (userFromDb == null)
        {
            throw new Exception("The user doesn't exist with specified login");
        }

        var isPasswordCorrect = VerifyPasswordHash(loginRequest.Password, userFromDb);

        if (!isPasswordCorrect)
        {
            throw new Exception("The specified password is wrong!");
        }
        
        var jwtToken = _jwtTokenService.GenerateToken(userFromDb.Login, userFromDb.Role.Name);

        return new AuthResult {Token = jwtToken};
    }
    
    private void GeneratePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            passwordSalt = hmac.Key;
        }
    }
    
    private bool VerifyPasswordHash(string inputPassword, User user)
    {
        using (var hmac = new HMACSHA512(user.PasswordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
            return computedHash.SequenceEqual(user.PasswordHash);
        }
    }
}