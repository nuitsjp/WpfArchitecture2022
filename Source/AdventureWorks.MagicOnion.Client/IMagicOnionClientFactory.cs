using MagicOnion;

namespace AdventureWorks.MagicOnion.Client;

/// <summary>
/// クライアントファクトリー
/// </summary>
public interface IMagicOnionClientFactory
{
    /// <summary>
    /// サービスクライアントを生成する。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T Create<T>() where T : IService<T>;
}