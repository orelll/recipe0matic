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

    [HttpPost(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get(IFormFile file, CancellationToken token)
    {
        //TODO: add file validation
        
        using (var ms = new MemoryStream())
        {
            await file.CopyToAsync(ms, token);
            var fileBytes = ms.ToArray();
            string s = Convert.ToBase64String(fileBytes);
            // act on the Base64 data
            
            var command = new AddFileCommand(file.Name, Guid.NewGuid().ToString(), FileContent: s);
            await _commandDispatcher.Send(command, token);
        }
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}