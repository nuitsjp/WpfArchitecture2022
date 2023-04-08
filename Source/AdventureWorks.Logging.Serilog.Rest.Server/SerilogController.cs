using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.Rest.Server;

[ApiController]
[Route("[controller]")]
public class SerilogController : ControllerBase, ISerilogService
{
    private readonly ILogger<SerilogController> _logger;

    public SerilogController(ILogger<SerilogController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "SerilogConfig")]
    public async Task<SerilogConfig> GetSerilogConfigAsync()
    {
        return new SerilogConfig(LogEventLevel.Information);
    }
}