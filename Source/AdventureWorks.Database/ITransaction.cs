using System.Data;

namespace AdventureWorks.Database;

/// <summary>
/// トランザクション
/// </summary>
public interface ITransaction : IDisposable
{
    /// <summary>
    /// データベース接続
    /// </summary>
    IDbConnection Connection { get; }
    /// <summary>
    /// トランザクションの終了をマークする。
    /// </summary>
    void Complete();
}