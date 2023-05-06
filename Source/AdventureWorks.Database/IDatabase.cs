using System.Data;
// ReSharper disable UnusedMemberInSuper.Global

namespace AdventureWorks.Database;

/// <summary>
/// データベース
/// </summary>
public interface IDatabase
{
    /// <summary>
    /// トランザクションを開始する。
    /// </summary>
    /// <returns></returns>
    ITransaction BeginTransaction();
    /// <summary>
    /// データベース接続を開く。トランザクションは適用されない為、読み取り時にのみ利用する。
    /// </summary>
    /// <returns></returns>
    IDbConnection Open();
}