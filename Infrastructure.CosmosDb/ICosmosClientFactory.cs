using Microsoft.Azure.Cosmos;

namespace Infrastructure.CosmosDb;

public interface ICosmosClientFactory
{
    void Init();
    
    public CosmosClient Client(string containerName, string databaseName);
    public Container Container(string containerName, string databaseName);
}