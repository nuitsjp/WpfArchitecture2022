using AdventureWorks.Authentication.MagicOnion.Server;
using Serilog.Core;
using Serilog.Events;

namespace AdventureWorks.Hosting.MagicOnion.Server;

public class EmployeeIdEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (ServerAuthenticationContext.Instance.IsAuthenticated)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "EmployeeId", ServerAuthenticationContext.Instance.CurrentUser.EmployeeId.AsPrimitive()));
        }
    }
}