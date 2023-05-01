using AdventureWorks.Logging;

namespace AdventureWorks.Purchasing.App.Driver.Scenario.RePurchasingTest.再発注する.Logging;

public class LoggingInitializer : ILoggingInitializer
{
    public async Task<bool> TryInitializeAsync()
    {
        await Task.CompletedTask;
        return true;
    }
}