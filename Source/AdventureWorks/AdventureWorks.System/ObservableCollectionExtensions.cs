using System.Collections.ObjectModel;

namespace AdventureWorks;

public static class ObservableCollectionExtensions
{
    public static void Replace<T>(this ObservableCollection<T> target, IEnumerable<T> items)
    {
        target.Clear();
        foreach (var item in items)
        {
            target.Add(item);
        }
    }
}