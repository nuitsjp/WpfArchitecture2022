using AdventureWorks.Authentication.MagicOnion.Server;
using MagicOnion.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace AdventureWorks.AspNetCore.MagicOnion
{
    public class ApplicationBuilder : AspNetCore.ApplicationBuilder, Hosting.Server.IApplicationBuilder
    {
        private readonly List<Assembly> _serviceAssemblies = new();

        public ApplicationBuilder(WebApplicationBuilder builder) : base(builder)
        {
        }

        public void Add(Assembly serviceAssembly)
        {
            if (_serviceAssemblies.Contains(serviceAssembly))
            {
                return;
            }
            _serviceAssemblies.Add(serviceAssembly);
        }

        public override IHost Build(string applicationName)
        {
            Builder.Services.AddGrpc();
            Builder.Services.AddMagicOnion(
                _serviceAssemblies.ToArray(),
                options =>
                {
                    options.GlobalFilters.Add<AuthenticationAttribute>();
                });

            var app = (WebApplication)base.Build(applicationName);

            app.MapMagicOnionService();

            return app;
        }
    }
}