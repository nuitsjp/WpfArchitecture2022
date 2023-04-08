using System.Text;
using AdventureWorks.Logging.Serilog.Rest;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Http;

namespace AdventureWorks.Logging.Serilog.Hosting.Wpf;

public static class Initializer
{
    public static async Task InitializeAsync(string applicationName)
    {
        var repository = new SerilogConfigRepository();
        var config = await repository.GetClientSerilogConfigAsync(applicationName);
#if DEBUG
        var minimumLevel = LogEventLevel.Debug;
#else
        var var maximumLevel = config.MinimumLevel;
#endif
        var settingString = config.Settings
            .Replace("%MinimumLevel%", minimumLevel.ToString())
            .Replace("%ApplicationName%", applicationName);

        using var settings = new MemoryStream(Encoding.UTF8.GetBytes(settingString));
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonStream(settings)
            .Build();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string>("apiKey", "secret-api-key")
            }!)
            .Build();
        
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configurationRoot)
#if DEBUG
            .WriteTo.Debug()
#endif
            .WriteTo.Http(
                requestUri: "https://localhost:3001/log-events",
                queueLimitBytes: null,
                httpClient: new CustomHttpClient(),
                configuration: configuration)
            .CreateLogger();
    }
}

public class CustomHttpClient : IHttpClient
{
    private static readonly HttpClient HttpClient = new(new HttpClientHandler { UseDefaultCredentials = true });

    public void Configure(IConfiguration configuration) => HttpClient.DefaultRequestHeaders.Add("X-Api-Key", configuration["apiKey"]);

    public async Task<HttpResponseMessage> PostAsync(string requestUri, Stream contentStream)
    {
        using var content = new StreamContent(contentStream);
        content.Headers.Add("Content-Type", "application/json");

        var response = await HttpClient
            .PostAsync(requestUri, content)
            .ConfigureAwait(false);

        return response;
    }

    public void Dispose()
    {
    }
}