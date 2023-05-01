namespace AdventureWorks.Business.Purchasing.SqlServer;

public class PurchasingDatabase : Database.Database
{
    /// <summary>
    /// インスタンスを生成する。サーバーサイドでしか利用しない為、パスワードを直接保持する。
    /// このソースにアクセスできる人は、本番環境の設定値を認識していることが前提にある。
    /// </summary>
    public PurchasingDatabase(string userId, string password) : base(userId, password)
    {
    }
}