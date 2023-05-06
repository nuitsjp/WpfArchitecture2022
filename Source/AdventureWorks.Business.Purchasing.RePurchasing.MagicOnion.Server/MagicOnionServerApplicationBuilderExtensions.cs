using AdventureWorks.Hosting.MagicOnion.Server;

namespace AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Server;

/// <summary>
/// 初期化サービス
/// </summary>
public static class MagicOnionServerApplicationBuilderExtensions
{
    /// <summary>
    /// 再発注サーバーを初期化する
    /// </summary>
    /// <param name="builder"></param>
    public static void UseRePurchasingMagicOnionServer(this IMagicOnionServerApplicationBuilder builder)
    {
        builder.AddServiceAssembly(typeof(MagicOnionServerApplicationBuilderExtensions).Assembly);
    }
}