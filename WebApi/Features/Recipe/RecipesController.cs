using Application.Features.Recipes.Create;
using Application.Features.Recipes.Get;
using Application.Features.Recipes.List;
using Domain.Abstractions.Commands;
using Domain.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using OneOf;
using WebApi.Features.Recipe.AddRecipe;
using WebApi.Features.Recipe.Models;

namespace WebApi.Features.Recipe.Get;

public static class RecipesRouting
{
    public static void AddRecipeGetEndpoint(this WebApplication app)
    {
        var prefix = "/recipes";

        app.MapGet(prefix + "/{id}", async (
                string id,
                [FromServices]IQueryBus queryBus,
                CancellationToken token) =>
            {
                var query = new GetRecipeQuery(id);
                var recipe = await queryBus.Query<GetRecipeQuery, OneOf<Application.Features.Recipes.Get.Recipe, RecipeNotFound, RecipeNotReady>>(query, token);

                return recipe.Match<IResult>(
                    found => Results.Ok(new RecipeModelDto
                    {
                        Id = query.RecipeId
                    }),
                    notFound => Results.NotFound(),
                    notReady => Results.BadRequest());
            })
            .Produces<IResult>()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Generate transaction token", Description = "Generate transaction token basing on key"
            });
        
        app.MapGet(prefix, async (
                [FromServices]IQueryBus queryBus,
                CancellationToken token) =>
            {
                var query = new ListRecipesQuery();
                var recipes = await queryBus.Query<ListRecipesQuery, IEnumerable<Domain.Features.Recipe>>(query, token);

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
            })
            .Produces<IResult>()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Generate transaction token", Description = "Generate transaction token basing on key"
            });        
        
        app.MapPost(prefix, async (
                [FromBody]CreateRecipeDto recipe,
                [FromServices]ICommandBus commandBus,
                CancellationToken token) =>
            {
                var newId = Guid.NewGuid().ToString("n");
                var command = new CreateRecipeCommand(newId, recipe.Name, recipe.Tags);
                await commandBus.Send(command, token);
        
                return Results.Created("/recipes/" + newId, null); 
            })
            .Produces<IResult>()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Generate transaction token", Description = "Generate transaction token basing on key"
            });
    }
}