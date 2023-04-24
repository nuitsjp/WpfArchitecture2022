//using System.Diagnostics;
//using Microsoft.Extensions.Logging;

//namespace AdventureWorks.Logging.Serilog.MagicOnion.Client;

//public class LoggerProviderProxy : ILoggerProvider
//{
//    public static ILoggerProvider Provider { get; set; } = new DebugLoggerProvider();
//    public ILogger CreateLogger(string categoryName)
//    {
//        return Provider.CreateLogger(categoryName);
//    }

//    public void Dispose()
//    {
//    }

//    private class DebugLoggerProvider : ILoggerProvider
//    {
//        public ILogger CreateLogger(string categoryName)
//        {
//            return new DebugLogger();
//        }

//        public void Dispose()
//        {
//        }
//    }

//    private class DebugLogger : ILogger
//    {
//        public IDisposable BeginScope<TState>(TState state)
//        {
//            return null!;
//        }

//        public bool IsEnabled(LogLevel logLevel)
//        {
//            return true;
//        }

//        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
//            Func<TState, Exception, string> formatter)
//        {
//            Debug.WriteLine(formatter(state, exception));
//        }
//    }
//}