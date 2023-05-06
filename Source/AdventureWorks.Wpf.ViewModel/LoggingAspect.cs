using PostSharp.Aspects;
using PostSharp.Serialization;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AdventureWorks.Wpf.ViewModel;

/// <summary>
/// ViewModelのメソッド呼び出しをハンドルしてログ出力するアスペクト。
/// </summary>
[PSerializable]
public class LoggingAspect : OnMethodBoundaryAspect
{
    /// <summary>
    /// ロガー。DIコンテナーは利用できない為、ロガーの初期化時に外部から設定させる。
    /// </summary>
    public static ILogger Logger { get; set; } = new NullLogger<LoggingAspect>();

    /// <summary>
    /// メソッド入場時の呼び出し。
    /// </summary>
    /// <param name="args"></param>
    public override void OnEntry(MethodExecutionArgs args)
    {
        var logLevel = GetLogLevel(args);
        Logger.Log(logLevel, "{Type}.{Method}({Args}) Entry", args.Method.ReflectedType!.FullName, args.Method.Name, args);
    }

    /// <summary>
    /// メソッド正常終了時の呼び出し。
    /// </summary>
    /// <param name="args"></param>
    public override void OnSuccess(MethodExecutionArgs args)
    {
        Logger.LogTrace("{Type}.{Method}({Args}) Success", args.Method.ReflectedType!.FullName, args.Method.Name, args);
    }

    /// <summary>
    /// メソッド終了時の呼び出し。
    /// </summary>
    /// <param name="args"></param>
    public override void OnExit(MethodExecutionArgs args)
    {
        // OnSuccessとOnExceptionで処理する。
    }

    /// <summary>
    /// メソッドで例外発生時の呼び出し。
    /// </summary>
    /// <param name="args"></param>
    public override void OnException(MethodExecutionArgs args)
    {
        Logger.LogTrace(args.Exception, "{Type}.{Method}({Args}) Exception", args.Method.ReflectedType!.FullName, args.Method.Name, args);
    }

    /// <summary>
    /// ログレベルを取得する。
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private static LogLevel GetLogLevel(MethodExecutionArgs args)
    {
        // On～メソッドと、RelayCommandAttributeが付与されたメソッドはDebugレベルで出力する。
        // 画面遷移イベントハンドラーがOn～という名称のため。
        // 基本的に命名規則でそれ以外にOn～は利用しないルールとするが、少々漏れても許容する。
        return args.Method.Name.StartsWith("On")
               || args.Method.GetCustomAttributes(true).Any(a => a is RelayCommandAttribute)
            ? LogLevel.Debug
            : LogLevel.Trace;
    }
}