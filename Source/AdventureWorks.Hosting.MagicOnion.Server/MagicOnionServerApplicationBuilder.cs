using System.Reflection;
using System.Text;
using AdventureWorks.Authentication;
using AdventureWorks.Authentication.MagicOnion.Server;
using AdventureWorks.Logging.Serilog;
using AdventureWorks.Logging.Serilog.SqlServer;
using AdventureWorks.MagicOnion.Server;
using MagicOnion.Server;
using MessagePack;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Serilog;

namespace AdventureWorks.Hosting.MagicOnion.Server
{
    public class MagicOnionServerApplicationBuilder : IMagicOnionServerApplicationBuilder
    {
        protected readonly WebApplicationBuilder Builder;

        private readonly FormatterResolverCollection _resolvers = new();

        private readonly List<Assembly> _serviceAssemblies = new();

        public MagicOnionServerApplicationBuilder(WebApplicationBuilder builder)
        {
            Builder = builder;
        }

        public IServiceCollection Services => Builder.Services;
        public IConfiguration Configuration => Builder.Configuration;

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

        public async Task<WebApplication> BuildAsync(ApplicationName applicationName)
        {
            Builder.Configuration.SetBasePath(Path.GetDirectoryName(Environment.ProcessPath!)!);

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

            // Serilogの初期化
            await InitializeSerilogAsync(applicationName);

            var app = Builder.Build();
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.MapMagicOnionService();

            return app;
        }

        private async Task InitializeSerilogAsync(ApplicationName applicationName)
        {
            var database = new SerilogDatabase();
            var repository = (ISerilogConfigRepository)new SerilogConfigRepository(database);
            var config = await repository.GetServerSerilogConfigAsync(applicationName);

#if DEBUG
            var minimumLevel = LogEventLevel.Debug;
#else
            var maximumLevel = config.MinimumLevel;
#endif
            var settingString = config.Settings
                .Replace("%ConnectionString%", database.ConnectionString)
                .Replace("%MinimumLevel%", minimumLevel.ToString())
                .Replace("%ApplicationName%", applicationName.Value);

            using var settings = new MemoryStream(Encoding.UTF8.GetBytes(settingString));
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(settings)
                .Build();

            Serilog.Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configurationRoot)
                .Enrich.With<EmployeeIdEnricher>()
#if DEBUG
                .WriteTo.Debug()
#endif
                .CreateLogger();

            Builder.Host.UseSerilog();
        }

    }
}