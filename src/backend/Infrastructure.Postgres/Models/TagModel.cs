using Domain;

namespace Infrastructure.Postgres.Models;

public class TagModel
{
    public string Id { get; set; }
    public string Value { get; set; }
    
    public static TagModel ToStorageModel(Tag model) => new ()
    {
        Id = model.Id,
        Value = model.Value
    };
    
    public static Tag FromStorageModel(TagModel model) => new ()
    {
        Id = model.Id,
        Value = model.Value
    };
}