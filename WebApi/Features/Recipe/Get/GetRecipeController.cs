using Application.Features.Recipes.Get;
using Domain.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using WebApi.Features.Recipe.Models;

namespace WebApi.Features.Recipe.Get;

[ApiController]
[Route("[controller]")]
public class GetRecipeController : ControllerBase
{
    private readonly IQueryBus _queryBus;

    public GetRecipeController(IQueryBus queryBus)
    {
        _queryBus = queryBus;
    }
    
    [HttpGet(Name = "GetRecipe")]
    public async Task<IActionResult> GetRecipe(string recipeId, CancellationToken token)
    {
        var query = new GetRecipeQuery(recipeId);
        var recipe = await _queryBus.Query<GetRecipeQuery, OneOf<Application.Features.Recipes.Get.Recipe, RecipeNotFound, RecipeNotReady>>(query, token);

        return recipe.Match<IActionResult>(
            found => Ok(new RecipeModelDto
            {
                Id = found.Id
            }),
            notFound => NotFound(),
            notReady => BadRequest());
    }
}