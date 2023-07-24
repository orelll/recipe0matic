namespace WebApi.Features.Recipe.AddRecipe;

public class CreateRecipeDto
{
    public string Name { get; set; }
    public IEnumerable<string>? Tags { get; set; }
}