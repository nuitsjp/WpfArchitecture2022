using System.Collections.ObjectModel;

namespace AdventureWorks;

/// <summary>
/// ObservableCollectionに対する拡張メソッドクラス
/// </summary>
public static class ObservableCollectionExtensions
{
    /// <summary>
    /// ObservableCollectionの内容を置き換える
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="target"></param>
    /// <param name="items"></param>
    public static void Replace<T>(this ObservableCollection<T> target, IEnumerable<T> items)
    {
        target.Clear();
        foreach (var item in items)
        {
            target.Add(item);
        }
    }
}