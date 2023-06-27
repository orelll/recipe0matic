using Azure.Core;
using Azure.Identity;
using Microsoft.Azure.Cosmos;

namespace CosmosDbTester;

public class DataReader
{
    private readonly string _cosmosDbConfigurationEndpoint = "https://localhost:8081";
    private readonly string _cosmosDbConfigurationPrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
    
    public CosmosClient Client { get; private set; }

    public Container Container(string containerName, string databaseName)
    {
        return Client.GetContainer(databaseName, containerName);
    }

    public void Init()
    {
        Client = !string.IsNullOrEmpty(_cosmosDbConfigurationPrimaryKey) ? 
            new CosmosClient(_cosmosDbConfigurationEndpoint, _cosmosDbConfigurationPrimaryKey, ClientOptions) 
            : new CosmosClient(_cosmosDbConfigurationEndpoint, new DefaultAzureCredential(), ClientOptions);

    }
    
    public static CosmosClientOptions ClientOptions => new ()
    {
        ConnectionMode = ConnectionMode.Gateway,
        SerializerOptions = new CosmosSerializationOptions()
        {
            IgnoreNullValues = true,
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        }
    };
}