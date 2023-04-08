using System.Net.Http;
using System.Net.Http.Json;

namespace AdventureWorks.Logging.Serilog.Rest;

public class SerilogConfigRepository : ISerilogConfigRepository
{
    private static readonly HttpClient HttpClient = new(new HttpClientHandler { UseDefaultCredentials = true });

    public Task<SerilogConfig> GetServerSerilogConfigAsync(string applicationName)
    {
        throw new NotImplementedException();
    }

    public async Task<SerilogConfig> GetClientSerilogConfigAsync(string applicationName)
    {
        var apiUrl = $"https://localhost:3001/SerilogConfig/{applicationName}";
        var response = await HttpClient.GetAsync(apiUrl);

        response.EnsureSuccessStatusCode();

        return (await response.Content.ReadFromJsonAsync<SerilogConfig>())!;
    }
}