using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Blob;

public static class BlobBootstrapper
{
    public static void ConfigureBlobStorage(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BlobConfiguration>(opt => configuration.GetSection(BlobConfiguration.Position).Bind(opt));
        
        services.AddScoped<BlobContainerClientProvider>();
    }
}