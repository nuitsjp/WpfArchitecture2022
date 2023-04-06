using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Hosting;
public interface IApplicationBuilder
{
    IServiceCollection Services { get; }
    IConfiguration Configuration { get; }
}