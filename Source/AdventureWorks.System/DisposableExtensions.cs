namespace AdventureWorks;

/// <summary>
/// IDisposableに対する拡張メソッド
/// </summary>
public static class DisposableExtensions
{
    /// <summary>
    /// Disposeを呼び出す。どうしても例外が発生してほしくないときに利用する。
    /// </summary>
    /// <param name="disposable"></param>
    public static void DisposeQuiet(this IDisposable disposable)
    {
        try
        {
            disposable.Dispose();
        }
        catch
        {
            // ignore
        }
    }
}
