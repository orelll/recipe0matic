using Domain;
using Domain.File;
using Microsoft.Azure.Cosmos;

namespace Infrastructure.CosmosDb;

public class CosmosFileRepository:IFileRepository
{
    private readonly ICosmosClientFactory _cosmos;
    private readonly string _containerName = "FilesDB";
    private readonly string _databaseName = "BackupR";
    
    public CosmosFileRepository(ICosmosClientFactory cosmos)
    {
        _cosmos = cosmos;
    }
    
    public async Task InsertFile<T>(T file, CancellationToken? token = null) where T : FileBase
    {
        _cosmos.Init();
        var container = _cosmos.Container(_containerName, _databaseName);
        await container.CreateItemAsync(file, new PartitionKey(file.FileId), cancellationToken: token ?? default);
    }

    public async Task<T> GetFile<T>(string id, string partitionKey, CancellationToken? token = null) where T : FileBase
    {
        _cosmos.Init();
        var container = _cosmos.Container(_containerName, _databaseName);
        return await container.ReadItemAsync<T>(id, new PartitionKey(partitionKey), cancellationToken: token ?? default);
    }

    public Task<T> Find<T>(Func<T, bool> searchPredicate, CancellationToken? token = null) where T : FileBase
    {
        throw new NotImplementedException();
    }
}