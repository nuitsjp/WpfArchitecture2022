using AdventureWorks.Hosting;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

/// <summary>
/// 初期化処理拡張メソッドクラス
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// 購買サービスを初期化する。
    /// </summary>
    /// <param name="builder"></param>
    public static void UsePurchasingMagicOnion(this IApplicationBuilder builder)
    {
        builder.AddFormatterResolver(CustomResolver.Instance);
    }
}