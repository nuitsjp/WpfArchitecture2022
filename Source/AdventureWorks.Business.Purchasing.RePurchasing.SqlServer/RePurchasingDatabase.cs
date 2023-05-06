namespace AdventureWorks.Business.Purchasing.RePurchasing.SqlServer;

/// <summary>
/// 再発注データベース
/// </summary>
public class RePurchasingDatabase : Database.Database
{
    /// <summary>
    /// インスタンスを生成する。サーバーサイドでしか利用しない為、パスワードを直接保持する。
    /// このソースにアクセスできる人は、本番環境の設定値を認識していることが前提にある。
    /// </summary>
    public RePurchasingDatabase() : base("RePurchasing", "%&^h6cGpWW4Q*u")
    {
    }
}