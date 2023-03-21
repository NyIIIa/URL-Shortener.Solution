using Microsoft.EntityFrameworkCore;
using URL_Shortener.Client.Data;
using URL_Shortener.Client.Interfaces.Authentication;
using URL_Shortener.Client.Services.Authentication;

namespace URL_Shortener.Client.Extensions;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
        serviceCollection.AddScoped<IJwtTokenService, JwtTokenService>();
    }

    public static void AddDbContext(this IServiceCollection serviceCollection,
                                    ConfigurationManager configurationManager)
    {
        serviceCollection.AddDbContext<UrlShortenedDbContext>(options => 
            options.UseSqlServer(configurationManager.GetConnectionString("DefaultConnection")));
    }
}