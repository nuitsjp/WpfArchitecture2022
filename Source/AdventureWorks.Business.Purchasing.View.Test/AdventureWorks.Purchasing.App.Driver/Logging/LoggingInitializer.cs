using AdventureWorks.Logging;

namespace AdventureWorks.Purchasing.App.Driver.Logging;

public class LoggingInitializer : ILoggingInitializer
{
    public async Task<bool> TryInitializeAsync()
    {
        await Task.CompletedTask;
        return true;
    }
}