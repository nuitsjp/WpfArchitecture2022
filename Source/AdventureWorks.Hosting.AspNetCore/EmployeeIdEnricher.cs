using AdventureWorks.Authentication.Jwt.MagicOnion.Server;
using Serilog.Core;
using Serilog.Events;

namespace AdventureWorks.Hosting.AspNetCore;

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