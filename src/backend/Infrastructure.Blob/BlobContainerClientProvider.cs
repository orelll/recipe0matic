using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;

namespace Infrastructure.Blob;

public class BlobContainerClientProvider
{
    private readonly BlobConfiguration _config;

    public BlobContainerClientProvider(IOptions<BlobConfiguration> options)
    {
        _config = options.Value ?? throw new ArgumentNullException("options");
    }

    public BlobContainerClient BuildContainerClient()
    {
        var accountName = "devstoreaccount1";
        var adress = "http://127.0.0.1:10000";
        var containerName = "container-name";
        var accountKey = "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==";
        // var connectionString = $"DefaultEndpointsProtocol=http;AccountName={accountName};AccountKey={accountKey};BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1";
        var connectionString =
            "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;";
        // BlobServiceClient client = new(connectionString);


        // With account name and key
        var client = new BlobContainerClient(
            new Uri($"{adress}/{accountName}/{containerName}"),
            new StorageSharedKeyCredential(accountName, accountKey)
        );
        return client;
    }
}