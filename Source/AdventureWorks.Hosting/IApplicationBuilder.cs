using MessagePack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdventureWorks.Hosting;

/// <summary>
/// アプリケーションビルダー。アプリケーションの初期化を抽象化し、各コンポーネントの初期化処理を再利用するために用いる。
/// </summary>
public interface IApplicationBuilder
{
    /// <summary>
    /// サービス
    /// </summary>
    IServiceCollection Services { get; }
    /// <summary>
    /// コンフィギュレーション
    /// </summary>
    IConfiguration Configuration { get; }
    /// <summary>
    /// ホスト
    /// </summary>
    IHostBuilder Host { get; }
    /// <summary>
    /// IFormatterResolverを追加する。
    /// </summary>
    /// <param name="resolver"></param>
    void AddFormatterResolver(IFormatterResolver resolver);
}