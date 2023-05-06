namespace AdventureWorks.Logging.Serilog.SqlServer;

/// <summary>
/// Serilogデータベース
/// </summary>
public class SerilogDatabase : Database.Database
{
    /// <summary>
    /// インスタンスを生成する。サーバーサイドでしか利用しない為、パスワードを直接保持する。
    /// </summary>
    public SerilogDatabase() : base("Serilog", "RG3CbVP!2U4hT5")
    {
    }
}