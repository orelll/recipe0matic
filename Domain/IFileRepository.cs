using Domain.File;

namespace Domain;

public interface IFileRepository
{
    Task InsertFile<T>(T file, CancellationToken? token = null) where T: FileBase;
    Task<T> GetFile<T>(string id, string partitionKey, CancellationToken? token = null) where T: FileBase;
    Task<T> Find<T>(Func<T, bool> searchPredicate, CancellationToken? token = null) where T: FileBase;
}