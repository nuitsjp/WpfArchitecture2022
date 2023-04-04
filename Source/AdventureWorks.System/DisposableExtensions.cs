namespace AdventureWorks;

public static class DisposableExtensions
{
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
