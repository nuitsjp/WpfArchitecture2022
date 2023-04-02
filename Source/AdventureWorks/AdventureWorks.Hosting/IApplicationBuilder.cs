using MessagePack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdventureWorks.Hosting;
public interface IApplicationBuilder
{
    IServiceCollection Services { get; }
    IConfiguration Configuration { get; }
    void Add(IFormatterResolver resolver);
}
