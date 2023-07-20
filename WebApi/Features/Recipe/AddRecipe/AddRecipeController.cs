using Application.Features.Recipes.Create;
using Domain.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Features.Recipe.AddRecipe;

[ApiController]
[Route("[controller]")]
public class AddFileController : ControllerBase
{
    private readonly ICommandBus _commandBus;

    public AddFileController(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    [HttpPost(Name = "AddRecipe")]
    public async Task<IActionResult> AddRecipe(CreateRecipeDto recipe, CancellationToken token)
    {
        var newId = Guid.NewGuid().ToString("n");
        var command = new CreateRecipeCommand(newId, recipe.Name, recipe.Tags);
        await _commandBus.Send(command, token);
        
        return Created("/recipes/" + newId, null); 
    }
}