using System.Reflection;
using AdventureWorks.Authentication;
using AdventureWorks.Authentication.Jwt.MagicOnion.Server;
using AdventureWorks.Hosting.AspNetCore;
using MagicOnion.Server;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Hosting.MagicOnion.Server
{
    public class MagicOnionServerApplicationBuilder : AspNetCoreApplicationBuilder, IMagicOnionServerApplicationBuilder
    {
        private readonly List<IFormatterResolver> _resolvers = new();

        private readonly List<Assembly> _serviceAssemblies = new();

        public MagicOnionServerApplicationBuilder(WebApplicationBuilder builder) : base(builder)
        {
        }

        public static MagicOnionServerApplicationBuilder CreateBuilder(string[] args) => new(WebApplication.CreateBuilder(args));

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

        public override async Task<WebApplication> BuildAsync(string applicationName)
        {
            Builder.Services.AddGrpc();
            Builder.Services.AddMagicOnion(
                _serviceAssemblies.ToArray(),
                options =>
                {
                    options.GlobalFilters.Add<AuthenticationAttribute>();
                });

            // MagicOnionの初期化
            _resolvers.Insert(0, StandardResolver.Instance);
            _resolvers.Add(ContractlessStandardResolver.Instance);
            StaticCompositeResolver.Instance.Register(_resolvers.ToArray());
            MessagePackSerializer.DefaultOptions = ContractlessStandardResolver.Options
                .WithResolver(StaticCompositeResolver.Instance);

            Services.AddSingleton<IAuthenticationContext, AuthenticationContext>();

            var app = await base.BuildAsync(applicationName);
            app.MapMagicOnionService();
            return app;
        }
    }
}