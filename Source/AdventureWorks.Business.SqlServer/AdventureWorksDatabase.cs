namespace AdventureWorks.Business.SqlServer;

/// <summary>
/// AdventureWorksドメインのデータベース
/// </summary>
public class AdventureWorksDatabase : Database.Database
{
    /// <summary>
    /// インスタンスを生成する。サーバーサイドでしか利用しない為、パスワードを直接保持する。
    /// このソースにアクセスできる人は、本番環境の設定値を認識していることが前提にある。
    /// </summary>
    public AdventureWorksDatabase() : base("AdventureWorks", "xR^g*BV2XX8d2p77")
    {
    }
}