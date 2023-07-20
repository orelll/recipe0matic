namespace WebApi.Features.Recipe.Models;

public class RecipeModelDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<TagModelDto> Tags { get; set; }
}