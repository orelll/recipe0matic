using Domain.Features;

namespace Infrastructure.Postgres.Models;

public class RecipeModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IList<IngredientModel>? Ingredients { get; set; }
    public IList<TagModel>? Tags { get; set; }
    
    public static Recipe? FromStorageModel(RecipeModel? model) => model != null ?  new (){
        Id = model.Id,
        Name = model.Name,
        Ingredients = model.Ingredients?.Select(IngredientModel.FromStorageModel).ToList(),
        Tags = model.Tags?.Select(TagModel.FromStorageModel).ToList()
    } : null;
    
    public static RecipeModel ToStorageModel(Recipe model) => new (){
        Id = model.Id,
        Name = model.Name,
        Tags = model.Tags?.Select(TagModel.ToStorageModel).ToList(),
        Ingredients = model.Ingredients?.Select(IngredientModel.ToStorageModel).ToList()
    };
}