using System.Configuration;
using System.Text;
using AdventureWorks.Database;
using AdventureWorks.Logging.Serilog;
using AdventureWorks.Logging.Serilog.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AdventureWorks.Hosting.AspNetCore;

public abstract class AspNetCoreApplicationBuilder : IApplicationBuilder
{
    protected readonly WebApplicationBuilder Builder;

    protected AspNetCoreApplicationBuilder(WebApplicationBuilder builder)
    {
        Builder = builder;
    }

    public IServiceCollection Services => Builder.Services;
    public IConfiguration Configuration => Builder.Configuration;

    public virtual async Task<WebApplication> BuildAsync(string applicationName)
    {
        Builder.Configuration.SetBasePath(Path.GetDirectoryName(Environment.ProcessPath!)!);

        await Logging.Serilog.Hosting.AspNetCore.Initializer.InitializeAsync(applicationName);

        Builder.Host.UseSerilog();

        var app = Builder.Build();
        app.UseHttpsRedirection();
        app.UseSerilogRequestLogging();

        return app;
    }
}