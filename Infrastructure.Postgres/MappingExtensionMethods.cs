using Domain;
using Domain.Features;
using Infrastructure.Postgres.Models;

namespace Infrastructure.Postgres;

public static class MappingExtensionMethods
{
    public static Recipe FromStorageModel(this RecipeModel model) => new (){
        Id = model.Id,
        Name = model.Name,
        Tags = model.Tags?.Select(FromStorageModel).ToList()
    };
    
    public static RecipeModel ToStorageModel(this Recipe model) => new (){
        Id = model.Id,
        Name = model.Name,
        Tags = model.Tags?.Select(ToStorageModel).ToList()
    };
    
    public static TagModel ToStorageModel(this Tag model) => new ()
    {
        Id = model.Id,
        Value = model.Value
    };
    
    public static Tag FromStorageModel(this TagModel model) => new ()
    {
        Id = model.Id,
        Value = model.Value
    };
}