using System.Diagnostics;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System.Reflection;
using CommunityToolkit.Mvvm.Input;

namespace AdventureWorks.Wpf.ViewModel;

[PSerializable]
public class LoggingAspect : OnMethodBoundaryAspect
{
    public static IViewModelLogger Logger { get; set; } = new NullLogger();

    public override void OnEntry(MethodExecutionArgs args)
    {
        if (IsMethodMatching(args.Method))
        {
            Logger.LogEntry(args.Method, args.Arguments.ToArray());
        }
    }

    public override void OnSuccess(MethodExecutionArgs args)
    {
        if (IsMethodMatching(args.Method))
        {
            Logger.LogSuccess(args.Method, args.Arguments.ToArray());
        }
    }

    public override void OnExit(MethodExecutionArgs args)
    {
        // OnSuccessとOnExceptionで処理する。
    }

    public override void OnException(MethodExecutionArgs args)
    {
        if (IsMethodMatching(args.Method))
        {
            Logger.LogException(args.Method, args.Exception, args.Arguments.ToArray());
        }
    }

    private class NullLogger : IViewModelLogger
    {
        public void LogEntry(MethodBase method, object[] args)
        {
            Debug.WriteLine($"{method.ReflectedType!.FullName}.{method.Name} Entry");
        }

        public void LogSuccess(MethodBase method, object[] args)
        {
            Debug.WriteLine($"{method.ReflectedType!.FullName}.{method.Name} Success");
        }

        public void LogException(MethodBase method, Exception exception, object[] args)
        {
            Debug.WriteLine($"{method.ReflectedType!.FullName}.{method.Name} Exception");
            Debug.WriteLine(exception.StackTrace);
        }
    }

    private static bool IsMethodMatching(MethodBase method)
    {
        return method.Name.StartsWith("On") 
               || method.GetCustomAttributes(true).Any(a => a is RelayCommandAttribute);
    }
}