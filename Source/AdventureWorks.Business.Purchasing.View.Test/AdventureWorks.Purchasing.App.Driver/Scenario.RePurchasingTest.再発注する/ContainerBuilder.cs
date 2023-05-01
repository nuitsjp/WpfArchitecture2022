using AdventureWorks.Authentication;
using AdventureWorks.Business.Purchasing.RePurchasing;
using AdventureWorks.Business.Purchasing;
using AdventureWorks.Logging;
using AdventureWorks.Purchasing.App.Driver.Scenario.RePurchasingTest.再発注する.Authentication;
using AdventureWorks.Purchasing.App.Driver.Scenario.RePurchasingTest.再発注する.Logging;
using AdventureWorks.Purchasing.App.Driver.Scenario.RePurchasingTest.再発注する.Purchasing;

namespace AdventureWorks.Purchasing.App.Driver.Scenario.RePurchasingTest.再発注する;

public class ContainerBuilder : IContainerBuilder
{
    public void Build(IServiceCollection services)
    {
        // 認証サービスを初期化する。
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IAuthenticationContext, AuthenticationContext>();

        // ロギングサービスを初期化する。
        services.AddTransient<ILoggingInitializer, LoggingInitializer>();

        // 購買サービスのクライアントを初期化する。
        services.AddTransient<IShipMethodRepository, ShipMethodRepository>();
        services.AddTransient<IVendorRepository, VendorRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepository>();
        services.AddTransient<IRequiringPurchaseProductQuery, RequiringPurchaseProductQuery>();
    }
}