using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.SqlServer;

/// <summary>
/// 初期化処理拡張メソッドクラス
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// 初期化処理を実行する
    /// </summary>
    /// <param name="builder"></param>
    public static void UseBusinessSqlServer(this IApplicationBuilder builder)
    {
        TypeHandlerInitializer.Initialize();

        builder.Services.AddTransient<AdventureWorksDatabase>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
    }
}