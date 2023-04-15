namespace AdventureWorks.Logging
{
    public interface ILoggingInitializer
    {
        Task<bool> TryInitializeAsync();
    }
}