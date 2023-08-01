namespace Infrastructure.CosmosDb;

public class CosmosDbConfiguration
{
    public static string Position = "CosmosDB";
    public string? Endpoint { get; set; }
    public string? PrimaryKey { get; set; }
}