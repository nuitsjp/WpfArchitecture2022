using System.Reflection;

namespace AdventureWorks.Wpf.ViewModel;

public interface IViewModelLogger
{
    void LogEntry(MethodBase method, object[] args);
    void LogSuccess(MethodBase method, object[] args);
    void LogException(MethodBase method, Exception exception, object[] args);
}