using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Infrastructure.CosmosDb;

public class CosmosClientFactory: ICosmosClientFactory
{
    private readonly CosmosDbConfiguration _config;
    private CosmosClient? _client;
    
    public CosmosClientFactory(IOptions<CosmosDbConfiguration> options)
    {
        _config = options.Value ?? GetTemporary;
    }
    
    public CosmosClient Client(string containerName, string databaseName)
    {
        return _client ?? throw new ArgumentNullException("CosmosClient");
    }

    public Container Container(string containerName, string databaseName)
    {
        return _client?.GetContainer(databaseName, containerName) ?? throw new ArgumentNullException("CosmosClient");
    }

    public void Init()
    {
        if (_client == null)
        {
            _client = !string.IsNullOrEmpty(_config.PrimaryKey) ? 
                new CosmosClient(_config.Endpoint, _config.PrimaryKey, ClientOptions) 
                : new CosmosClient(_config.Endpoint, new DefaultAzureCredential(), ClientOptions);
        }
    }
    
    private CosmosDbConfiguration GetTemporary => new()
    {
        Endpoint = "https://localhost:8081",
        PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="
    };
    
    private static CosmosClientOptions ClientOptions => new ()
    {
        ConnectionMode = ConnectionMode.Gateway,
        SerializerOptions = new CosmosSerializationOptions()
        {
            IgnoreNullValues = true,
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        }
    };
}