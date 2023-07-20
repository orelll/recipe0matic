using Application.Features.Recipes.List;
using Domain.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Features.Recipe.Models;

namespace WebApi.Features.Recipe.ListRecipes;

[ApiController]
[Route("[controller]")]
public class ListFilesController : ControllerBase
{
    private readonly IQueryBus _queryBus;

    public ListFilesController(IQueryBus queryBus)
    {
        _queryBus = queryBus;
    }
    
    [HttpGet(Name = "ListSlimFiles")]
    public async Task<IEnumerable<RecipeModelDto>> AddFile(CancellationToken token)
    {
        var query = new ListRecipesQuery();
        var recipes = await _queryBus.Query<ListRecipesQuery, IEnumerable<Domain.Features.Recipe>>(query, token);

        return recipes.Select(r => new RecipeModelDto
        {
            Id = r.Id,
            Name = r.Name,
            Tags = r.Tags?.Select(t => new TagModelDto
            {
                Id = t.Id,
                Value = t.Value
            }).ToList()
        });
    }
}