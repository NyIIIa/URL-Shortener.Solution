using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using URL_Shortener.Client.Data;
using URL_Shortener.Client.Interfaces.Authentication;
using URL_Shortener.Client.Interfaces.ShortUrl;
using URL_Shortener.Client.Interfaces.UnitOfWork;
using URL_Shortener.Client.Models.Authentication;
using URL_Shortener.Client.Services.Authentication;
using URL_Shortener.Client.Services.ShortUrl;
using URL_Shortener.Client.Services.UnitOfWork;

namespace URL_Shortener.Client.Extensions;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection serviceCollection, 
                                        ConfigurationManager configurationManager)
    {
        serviceCollection.AddScoped<IShortUrlService, ShortUrlService>();
        serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
        serviceCollection.AddScoped<IJwtTokenService, JwtTokenService>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        serviceCollection.Configure<JwtSettings>(configurationManager.GetSection("JwtSettings"));
    }

    public static void AddLocalDbContext(this IServiceCollection serviceCollection,
                                    ConfigurationManager configurationManager)
    {
        serviceCollection.AddDbContext<UrlShortenedDbContext>(options => 
            options.UseSqlServer(configurationManager.GetConnectionString("DefaultConnection")));
    }
    
    public static void AddRemoteDbContext(this IServiceCollection serviceCollection,
                                    ConfigurationManager configurationManager)
    {
        serviceCollection.AddDbContext<UrlShortenedDbContext>(options => 
            options.UseSqlServer(configurationManager.GetConnectionString("RemoteConnection")));
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
                jwt.Events = new()
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("X-Access-Token"))
                        {
                            context.Token = context.Request.Cookies["X-Access-Token"];
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        serviceCollection.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", p => p.RequireRole("Admin"));
            options.AddPolicy("User", p => p.RequireRole("Admin", "User"));
        });
    }

    public static void InnitDatabase(this IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();

        serviceScope.ServiceProvider.GetService<UrlShortenedDbContext>().Database.Migrate();
    }
}