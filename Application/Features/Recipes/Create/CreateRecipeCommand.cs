namespace Application.Features.Recipes.Create;

public record CreateRecipeCommand(string Id, string Name, IEnumerable<string> Tags);