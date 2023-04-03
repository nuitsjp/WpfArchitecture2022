using MagicOnion.Server;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace AdventureWorks.AspNetCore.MagicOnion
{
    public class MagicOnionApplicationBuilder : ApplicationBuilder, Hosting.IMagicOnionServerApplicationBuilder
    {
        private readonly List<IFormatterResolver> _resolvers = new();

        private readonly List<Assembly> _serviceAssemblies = new();

        public MagicOnionApplicationBuilder(WebApplicationBuilder builder) : base(builder)
        {
        }

        public static MagicOnionApplicationBuilder CreateBuilder(string[] args) => new(WebApplication.CreateBuilder(args));

        public void AddFormatterResolver(IFormatterResolver resolver)
        {
            if (_resolvers.Contains(resolver))
            {
                return;
            }
            _resolvers.Add(resolver);
        }

        public void AddServiceAssembly(Assembly serviceAssembly)
        {
            if (_serviceAssemblies.Contains(serviceAssembly))
            {
                return;
            }
            _serviceAssemblies.Add(serviceAssembly);
        }

        public override WebApplication Build(string applicationName)
        {
            Builder.Services.AddGrpc();
            Builder.Services.AddMagicOnion(
                _serviceAssemblies.ToArray(),
                options =>
                {
                    options.GlobalFilters.Add<AuthenticationAttribute>();
                });

            _resolvers.Insert(0, StandardResolver.Instance);
            _resolvers.Add(ContractlessStandardResolver.Instance);
            StaticCompositeResolver.Instance.Register(_resolvers.ToArray());
            MessagePackSerializer.DefaultOptions = ContractlessStandardResolver.Options
                .WithResolver(StaticCompositeResolver.Instance);


            var app = base.Build(applicationName);
            app.MapMagicOnionService();
            return app;
        }
    }
}