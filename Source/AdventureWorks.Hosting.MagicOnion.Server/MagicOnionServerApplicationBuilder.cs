using System.Reflection;
using AdventureWorks.Authentication;
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
        private readonly FormatterResolverCollection _resolvers = new();

        private readonly List<Assembly> _serviceAssemblies = new();

        public MagicOnionServerApplicationBuilder(WebApplicationBuilder builder) : base(builder)
        {
        }

        public static MagicOnionServerApplicationBuilder CreateBuilder(string[] args) => new(WebApplication.CreateBuilder(args));

        public void AddFormatterResolver(IFormatterResolver resolver)
        {
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
            // MagicOnionの初期化
            _resolvers.InitializeResolver();

            Builder.Services.AddGrpc();
            Builder.Services.AddMagicOnion(
                _serviceAssemblies.ToArray(),
                options =>
                {
                    options.GlobalFilters.Add<AuthenticationFilterAttribute>();
                    options.GlobalFilters.Add<LoggingFilterAttribute>();
                });

            // サーバー用認証コンテキストをDIコンテナーに登録
            Services.AddSingleton<IAuthenticationContext>(ServerAuthenticationContext.Instance);

            var app = await base.BuildAsync(applicationName);
            app.MapMagicOnionService();
            return app;
        }
    }
}