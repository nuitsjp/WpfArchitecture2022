using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.Rest.Server;

[ApiController]
public class LogEventsController : ControllerBase
{
    private readonly ILogger<LogEventsController> _logger;

    public LogEventsController(ILogger<LogEventsController> logger)
    {
        this._logger = logger;
    }

    [HttpPost("log-events")]
    public void Post([FromBody] LogEvent[] body)
    {
        var nbrOfEvents = body.Length;
        var apiKey = Request.Headers["X-Api-Key"].FirstOrDefault();

        _logger.LogInformation(
            "Received batch of {count} log events from {sender}",
            nbrOfEvents,
            apiKey);

        foreach (var logEvent in body)
        {
            _logger.LogInformation("Message: {message}", logEvent.RenderedMessage);
        }
    }
}