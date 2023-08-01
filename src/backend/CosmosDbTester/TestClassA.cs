using Newtonsoft.Json;

namespace CosmosDbTester;

public class TestClassA
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "testId")]
    public string TestId { get; set; }
    public string Dupa { get; set; }
    public int TestInt { get; set; }
}