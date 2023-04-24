﻿using PostSharp.Aspects;
using PostSharp.Serialization;
using System.Reflection;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace AdventureWorks.Wpf.ViewModel;

[PSerializable]
public class LoggingAspect : OnMethodBoundaryAspect
{
    public static ILogger Logger { get; set; } = new NullLogger<LoggingAspect>();

    public override void OnEntry(MethodExecutionArgs args)
    {
        var logLevel = IsMethodMatching(args.Method) ? LogLevel.Debug : LogLevel.Trace;
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
        Logger.LogError(args.Exception, "{Type}.{Method}({Args}) Exception", args.Method.ReflectedType!.FullName, args.Method.Name, args);
    }
    private static bool IsMethodMatching(MethodBase method)
    {
        return method.Name.StartsWith("On") 
               || method.GetCustomAttributes(true).Any(a => a is RelayCommandAttribute);
    }
}