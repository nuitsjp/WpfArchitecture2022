using System.Configuration;
using System.Text;
using AdventureWorks.Database;
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

        var connectionString = ConnectionStringProvider.Resolve("sa", "P@ssw0rd!");
        await Logging.Serilog.Initializer.InitializeServerAsync(connectionString, applicationName);

        Builder.Host.UseSerilog();

        var app = Builder.Build();
        app.UseHttpsRedirection();
        app.UseSerilogRequestLogging();

        return app;
    }
}