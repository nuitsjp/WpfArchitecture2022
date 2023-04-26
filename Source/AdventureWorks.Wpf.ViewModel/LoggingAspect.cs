using PostSharp.Aspects;
using PostSharp.Serialization;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AdventureWorks.Wpf.ViewModel;

[PSerializable]
public class LoggingAspect : OnMethodBoundaryAspect
{
    public static ILogger Logger { get; set; } = new NullLogger<LoggingAspect>();

    public override void OnEntry(MethodExecutionArgs args)
    {
        var logLevel = GetLogLevel(args);
        Logger.Log(logLevel, "{Type}.{Method}({Args}) Entry", args.Method.ReflectedType!.FullName, args.Method.Name, args);
    }

    public override void OnSuccess(MethodExecutionArgs args)
    {
        Logger.LogTrace("{Type}.{Method}({Args}) Success", args.Method.ReflectedType!.FullName, args.Method.Name, args);
    }

    public override void OnExit(MethodExecutionArgs args)
    {
        // OnSuccessとOnExceptionで処理する。
    }

    public override void OnException(MethodExecutionArgs args)
    {
        Logger.LogTrace(args.Exception, "{Type}.{Method}({Args}) Exception", args.Method.ReflectedType!.FullName, args.Method.Name, args);
    }

    private static LogLevel GetLogLevel(MethodExecutionArgs args)
    {
        return args.Method.Name.StartsWith("On")
               || args.Method.GetCustomAttributes(true).Any(a => a is RelayCommandAttribute)
            ? LogLevel.Debug
            : LogLevel.Trace;
    }
}