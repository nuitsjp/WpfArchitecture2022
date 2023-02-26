﻿using System.Reflection;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdventureWorks.AspNetCore;
public class ApplicationBuilder : AdventureWorks.Extensions.IApplicationBuilder
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
    public void Add(Assembly serviceAssembly) => _serviceAssemblies.Add(serviceAssembly);
    public void Add(IFormatterResolver resolver) => _resolvers.Add(resolver);

    public IHost Build()
    {
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
        return app;
    }

    public static ApplicationBuilder CreateBuilder(string[] args) => new (WebApplication.CreateBuilder(args));
}