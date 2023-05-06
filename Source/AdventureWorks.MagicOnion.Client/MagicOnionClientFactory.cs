using AdventureWorks.Authentication;
using AdventureWorks.Authentication.MagicOnion.Client;
using Grpc.Net.Client;
using MagicOnion;
using MagicOnion.Client;

namespace AdventureWorks.MagicOnion.Client;

/// <summary>
/// MagicOnionクライアントファクトリー
/// </summary>
public class MagicOnionClientFactory : IMagicOnionClientFactory
{
    /// <summary>
    /// 認証コンテキスト
    /// </summary>
    private readonly IAuthenticationContext _authenticationContext;
    /// <summary>
    /// エンドポイント
    /// </summary>
    private readonly Endpoint _endpoint;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="authenticationContext"></param>
    /// <param name="endpoint"></param>
    public MagicOnionClientFactory(
        IAuthenticationContext authenticationContext,
        Endpoint endpoint)
    {
        _authenticationContext = authenticationContext;
        _endpoint = endpoint;
    }

    /// <summary>
    /// サービスクライアントを生成する。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Create<T>() where T : IService<T>
    {
        return MagicOnionClient.Create<T>(
            GrpcChannel.ForAddress(_endpoint.Uri),
            new IClientFilter[]
            {
                new AuthenticationFilter(_authenticationContext)
            });
    }

}