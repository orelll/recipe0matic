using Newtonsoft.Json;

namespace Domain.File;

public abstract class FileBase
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "fileId")]
    public string FileId { get; set; } = string.Empty;
}