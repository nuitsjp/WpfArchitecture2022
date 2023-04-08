using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.Rest.Server;

[ApiController]
[Route("[controller]")]
public class SerilogConfigController : ControllerBase, ISerilogConfigService
{
    private readonly ISerilogConfigRepository _repository;

    public SerilogConfigController(ISerilogConfigRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{applicationName}")]
    public Task<SerilogConfig> GetSerilogConfigAsync(string applicationName)
    {
        return _repository.GetClientSerilogConfigAsync(applicationName);
    }
}