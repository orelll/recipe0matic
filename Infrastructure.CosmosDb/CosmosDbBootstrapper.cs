using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CosmosDb;

public static class CosmosDbBootstrapper
{
    public static void ConfigureCosmosDb(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CosmosDbConfiguration>(opt => configuration.GetSection(CosmosDbConfiguration.Position).Bind(opt));
        
        services.AddScoped<ICosmosClientFactory, CosmosClientFactory>();
        services.AddScoped<IFileRepository, CosmosFileRepository>();
    }
}