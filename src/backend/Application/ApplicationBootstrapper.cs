using Application.Dispatchers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public class ApplicationBootstrapper
{
    public static void ConfigureApplication(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IQueryDispatcher, MasstransitMediatorQueryDispatcher>();
        services.AddScoped<ICommandDispatcher, MasstransitMediatorCommandDispatcher>();
    }
    
    public static void ConfigureMediator(IMediatorRegistrationConfigurator cfg)
    {
       
    }
}