namespace AdventureWorks;

/// <summary>
/// 環境関連の共通ライブラリ
/// </summary>
public static class Environments
{
    /// <summary>
    /// 環境変数から値を取得する。<br/>
    /// プロセス、ユーザー、システムの優先順位で取得し、いずれにも存在しなかった場合は、デフォルト値を返却する。<br/>
    /// 主にAPIの接続先の取得などに利用する。<br/>
    /// 環境による相違点を基本的には環境変数に追い出し、インストール時の環境変数登録などで対応する。
    /// </summary>
    /// <param name="variable"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static string GetEnvironmentVariable(string variable, string defaultValue)
    {
        return Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.Process)
               ?? Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.User)
               ?? Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.Machine)
               ?? defaultValue;
    }

}