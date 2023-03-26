using System.Reflection;
using System.Text;
using AdventureWorks.Database;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using IApplicationBuilder = AdventureWorks.Hosting.IApplicationBuilder;

namespace AdventureWorks.AspNetCore;
public class ApplicationBuilder : IApplicationBuilder
{
    private readonly List<IFormatterResolver> _resolvers = new();

    private readonly WebApplicationBuilder _builder;

    private readonly List<Assembly> _serviceAssemblies = new();

    public ApplicationBuilder(WebApplicationBuilder builder)
    {
        _builder = builder;
    }

    public IServiceCollection Services => _builder.Services;
    public IConfiguration Configuration => _builder.Configuration;
    public void Add(Assembly serviceAssembly)
    {
        if (_serviceAssemblies.Contains(serviceAssembly))
        {
            return;
        }
        _serviceAssemblies.Add(serviceAssembly);
    }

    public void Add(IFormatterResolver resolver)
    {
        if (_resolvers.Contains(resolver))
        {
            return;
        }
        _resolvers.Add(resolver);
    }

    public IHost Build(string applicationName)
    {
        _builder.Configuration.SetBasePath(Path.GetDirectoryName(Environment.ProcessPath!)!);

        InitializeSerilog(Configuration, applicationName);

        _builder.Host.UseSerilog();
        _builder.Services.AddGrpc();
        _builder.Services.AddMagicOnion(_serviceAssemblies.ToArray());

        _resolvers.Insert(0, StandardResolver.Instance);
        _resolvers.Add(ContractlessStandardResolver.Instance);
        StaticCompositeResolver.Instance.Register(_resolvers.ToArray());
        MessagePackSerializer.DefaultOptions = ContractlessStandardResolver.Options
            .WithResolver(StaticCompositeResolver.Instance);

        var app = _builder.Build();
        app.UseHttpsRedirection();
        app.MapMagicOnionService();
        app.UseSerilogRequestLogging();
        return app;
    }

    public static ApplicationBuilder CreateBuilder(string[] args) => new (WebApplication.CreateBuilder(args));

    public static void InitializeSerilog(IConfiguration configuration, string applicationName)
    {
        var connectionString = ConnectionStringProvider.Resolve(configuration, "sa", "P@ssw0rd!");
        var minimumLevel = GetMinimumLevel(connectionString, applicationName);
        var settingString = Properties.Resources.Serilog
            .Replace("%ConnectionString%", connectionString)
            .Replace("%MinimumLevel%", minimumLevel)
            .Replace("%ApplicationName%", applicationName);
        using var settings = new MemoryStream(Encoding.UTF8.GetBytes(settingString));
        var cc = new ConfigurationBuilder()
            .AddJsonStream(settings)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(cc)
#if DEBUG
            .WriteTo.Debug()
#endif
            .CreateLogger();
    }

    // ReSharper disable UnusedParameter.Local
    private static string GetMinimumLevel(string connectionString, string applicationName)
    {
#if DEBUG
        return "Debug";
#else
            const string defaultMinimumLevel = "Information";
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                return connection.QuerySingleOrDefault<string>(@"
select
	MinimumLevel
from
	[dbo].[LogSettings]
where
	ApplicationName = @ApplicationName",
                    new
                    {
                        ApplicationName = applicationName
                    }) ?? defaultMinimumLevel;
            }
            catch
            {
                return defaultMinimumLevel;
            }
#endif
    }

}