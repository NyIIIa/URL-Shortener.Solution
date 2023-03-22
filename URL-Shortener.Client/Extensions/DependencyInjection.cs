using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using URL_Shortener.Client.Data;
using URL_Shortener.Client.Interfaces.Authentication;
using URL_Shortener.Client.Interfaces.UnitOfWork;
using URL_Shortener.Client.Models.Authentication;
using URL_Shortener.Client.Services.Authentication;
using URL_Shortener.Client.Services.UnitOfWork;

namespace URL_Shortener.Client.Extensions;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection serviceCollection, 
                                        ConfigurationManager configurationManager)
    {
        serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
        serviceCollection.AddScoped<IJwtTokenService, JwtTokenService>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        serviceCollection.Configure<JwtSettings>(configurationManager.GetSection("JwtSettings"));
    }

    public static void AddDbContext(this IServiceCollection serviceCollection,
                                    ConfigurationManager configurationManager)
    {
        serviceCollection.AddDbContext<UrlShortenedDbContext>(options => 
            options.UseSqlServer(configurationManager.GetConnectionString("DefaultConnection")));
    }

    public static void AddJwt(this IServiceCollection serviceCollection, 
                              ConfigurationManager configurationManager)
    {
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(configurationManager.GetSection("JwtSettings:Secret").Value);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });
    }
}