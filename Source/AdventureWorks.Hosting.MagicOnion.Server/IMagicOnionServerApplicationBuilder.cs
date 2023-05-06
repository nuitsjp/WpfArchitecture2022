using System.Reflection;

namespace AdventureWorks.Hosting.MagicOnion.Server;

/// <summary>
/// MagicOnionサーバーのビルダー。
/// </summary>
public interface IMagicOnionServerApplicationBuilder : IApplicationBuilder
{
    /// <summary>
    /// MagicOnionサービスを追加する。
    /// </summary>
    /// <param name="serviceAssembly"></param>
    void AddServiceAssembly(Assembly serviceAssembly);
}