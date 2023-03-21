using URL_Shortener.Client.Interfaces.Authentication;
using URL_Shortener.Client.Services.Authentication;

namespace URL_Shortener.Client.Extensions;

public static class DependencyInjection
{
    public static void AddInfrastructure(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
        serviceCollection.AddScoped<IJwtTokenService, JwtTokenService>();
    }
}