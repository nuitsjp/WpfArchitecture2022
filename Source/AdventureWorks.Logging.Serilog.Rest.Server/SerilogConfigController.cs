using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.Rest.Server;

[ApiController]
[Route("[controller]")]
public class SerilogConfigController : ControllerBase, ISerilogConfigRepository
{
    private readonly ISerilogConfigRepository _repository;

    public SerilogConfigController(ISerilogConfigRepository repository)
    {
        _repository = repository;
    }

    [HttpGet(Name = "SerilogConfig")]
    public Task<SerilogConfig> GetServerSerilogConfigAsync(string applicationName)
    {
        return _repository.GetServerSerilogConfigAsync(applicationName);
    }
}