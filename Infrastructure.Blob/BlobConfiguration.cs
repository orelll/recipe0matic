namespace Infrastructure.Blob;

public class BlobConfiguration
{
    public static string Position = "Blob";
    public string AccountName { get; set; } = string.Empty;
    public string AccountKey { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ContainerName { get; set; } = string.Empty;
}