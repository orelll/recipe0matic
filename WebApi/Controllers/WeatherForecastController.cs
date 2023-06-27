using Application.Dispatchers;
using Application.Features.AddFile;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private IFileRepository _fileRepo;
    private ICommandDispatcher _commandDispatcher;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IFileRepository fileRepo, ICommandDispatcher commandDispatcher)
    {
        _logger = logger;
        _fileRepo = fileRepo;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken token)
    {
        var command = new AddFileCommand("DUPAAAA", Guid.NewGuid().ToString());
        _commandDispatcher.Send(command, token);
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}