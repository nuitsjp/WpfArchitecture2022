using AdventureWorks.Authentication.MagicOnion.Server;
using Serilog.Core;
using Serilog.Events;

namespace AdventureWorks.Hosting.MagicOnion.Server;

/// <summary>
/// ログに従業員IDを追加する。
/// </summary>
public class EmployeeIdEnricher : ILogEventEnricher
{
    /// <summary>
    /// ログに従業員IDを追加する。
    /// </summary>
    /// <param name="logEvent"></param>
    /// <param name="propertyFactory"></param>
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (ServerAuthenticationContext.Instance.IsAuthenticated)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "EmployeeId", ServerAuthenticationContext.Instance.CurrentUser.EmployeeId.AsPrimitive()));
        }
    }
}