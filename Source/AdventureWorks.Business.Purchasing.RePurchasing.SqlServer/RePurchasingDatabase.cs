namespace AdventureWorks.Business.Purchasing.RePurchasing.SqlServer;

public class RePurchasingDatabase : Database.Database
{
    /// <summary>
    /// インスタンスを生成する。サーバーサイドでしか利用しない為、パスワードを直接保持する。
    /// このソースにアクセスできる人は、本番環境の設定値を認識していることが前提にある。
    /// </summary>
    public RePurchasingDatabase(string userId, string password) : base(userId, password)
    {
    }
}