﻿using System.Reflection;
using System.Windows;
using AdventureWorks.Extensions;
using AdventureWorks.Serilog;
using AdventureWorks.Wpf.ViewModel;
using Kamishibai;
using MessagePack;
using MessagePack.Resolvers;
using Serilog;

namespace AdventureWorks.Wpf.Hosting;

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
    public IHost Build(string applicationName)
    {
        LoggerInitializer.InitializeForWpf(Configuration, applicationName);
        LoggingAspect.Logger = new ViewModelLogger();

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

public class ViewModelLogger : IViewModelLogger
{
    public void LogEntry(MethodBase method, object[] args)
    {
        Log.Debug("{Type}.{Method}({Args}) Entry", method.ReflectedType!.FullName, method.Name, args);
    }

    public void LogSuccess(MethodBase method, object[] args)
    {
        Log.Debug("{Type}.{Method}({Args}) Success", method.ReflectedType!.FullName, method.Name, args);
    }

    public void LogException(MethodBase method, Exception exception, object[] args)
    {
        Log.Debug(exception, "{Type}.{Method}({Args}) Exception", method.ReflectedType!.FullName, method.Name, args);
    }
}