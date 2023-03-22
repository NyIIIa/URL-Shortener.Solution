using Microsoft.EntityFrameworkCore;
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
}