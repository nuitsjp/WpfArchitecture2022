using Microsoft.Data.SqlClient;

namespace AdventureWorks.Database;

/// <summary>
/// 接続文字列プロバイダー
/// </summary>
public static class ConnectionStringProvider
{
    /// <summary>
    /// 接続文字列を解決する。
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string Resolve(string userId, string password)
    {
        return new SqlConnectionStringBuilder
        {
            DataSource = Environments.GetEnvironmentVariable("AdventureWorks.Database.DataSource", "localhost"),
            InitialCatalog = "AdventureWorks",
            UserID = userId,
            Password = password,
            TrustServerCertificate = true
        }.ToString();
    }
}