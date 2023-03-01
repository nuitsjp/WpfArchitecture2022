using System.Windows;
using AdventureWorks.Database;
using AdventureWorks.Extensions;
using Kamishibai;
using MessagePack;
using MessagePack.Resolvers;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace AdventureWorks.Wpf;

public class ApplicationBuilder<TApplication, TWindow> : IApplicationBuilder
    where TApplication : Application
    where TWindow : Window

{
    private readonly List<IFormatterResolver> _resolvers = new();

    private readonly IWpfApplicationBuilder<TApplication, TWindow> _applicationBuilder;

    public ApplicationBuilder(IWpfApplicationBuilder<TApplication, TWindow> applicationBuilder)
    {
        _applicationBuilder = applicationBuilder;
    }

    public IServiceCollection Services => _applicationBuilder.Services;
    public IConfiguration Configuration => _applicationBuilder.Configuration;
    public IHost Build()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .WriteTo.MSSqlServer(
                connectionString: ConnectionStringProvider.Resolve(this),
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = "LogEvents",
                    AutoCreateSqlTable = true
                })
            .CreateLogger();

        _resolvers.Insert(0, StandardResolver.Instance);
        _resolvers.Add(ContractlessStandardResolver.Instance);
        StaticCompositeResolver.Instance.Register(_resolvers.ToArray());
        MessagePackSerializer.DefaultOptions = ContractlessStandardResolver.Options
            .WithResolver(StaticCompositeResolver.Instance);

        return _applicationBuilder.Build();
    }

    public void Add(IFormatterResolver resolver)
    {
        if (_resolvers.Contains(resolver))
        {
            return;
        }
        _resolvers.Add(resolver);
    }

    public static ApplicationBuilder<TApplication, TWindow> CreateBuilder()
    {
        return new(KamishibaiApplication<TApplication, TWindow>.CreateBuilder());
    }
}