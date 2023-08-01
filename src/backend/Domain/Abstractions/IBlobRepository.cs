namespace Domain;

public interface IBlobRepository
{
    Task UploadFile(object file, byte[] fileContent, CancellationToken token = default);
    Task<byte[]> LoadFile(string id, CancellationToken token = default);
}