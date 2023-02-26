using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdventureWorks.AspNetCore;
public class ApplicationBuilder : AdventureWorks.Extensions.IApplicationBuilder
{
    private readonly WebApplicationBuilder _builder;

    private readonly List<Assembly> _serviceAssemblies = new();

    public ApplicationBuilder(WebApplicationBuilder builder)
    {
        _builder = builder;
    }

    public IServiceCollection Services => _builder.Services;
    public IConfiguration Configuration => _builder.Configuration;
    public void Add(Assembly serviceAssembly) => _serviceAssemblies.Add(serviceAssembly);
    public IHost Build()
    {
        _builder.Services.AddGrpc();
        _builder.Services.AddMagicOnion(_serviceAssemblies.ToArray());

        var app = _builder.Build();
        app.UseHttpsRedirection();
        app.MapMagicOnionService();
        return app;
    }
}