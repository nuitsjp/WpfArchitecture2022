using AdventureWorks.Hosting;

namespace AdventureWorks.Business.MagicOnion;

/// <summary>
/// 初期化処理拡張メソッドクラス
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// 初期化する。
    /// </summary>
    /// <param name="builder"></param>
    public static void UseBusinessMagicOnion(this IApplicationBuilder builder)
    {
        builder.AddFormatterResolver(CustomResolver.Instance);
    }
}
