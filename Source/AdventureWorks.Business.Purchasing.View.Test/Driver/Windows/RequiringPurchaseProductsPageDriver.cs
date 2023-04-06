using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;
using System.Windows.Controls;

namespace Driver.Windows
{
    [WindowDriver(TypeFullName = "AdventureWorks.Business.Purchasing.View.MainWindow")]
    public class RequiringPurchaseProductsPageDriver
    {
        public WindowControl Core { get; }
        public WPFButtonBase GoBackCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("GoBackCommand").FirstOrDefault()?.Dynamic(); 
        public WPFButtonBase PurchaseCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("PurchaseCommand").FirstOrDefault()?.Dynamic(); 
        public WPFTextBlock SelectedRequiringPurchaseProductVendorName => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("SelectedRequiringPurchaseProduct.VendorName").FirstOrDefault()?.Dynamic(); 
        public GcSpreadGridDriver RequiringPurchaseProducts => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("RequiringPurchaseProducts").FirstOrDefault()?.Dynamic(); 

        public RequiringPurchaseProductsPageDriver(WindowControl core)
        {
            Core = core;
        }

        public RequiringPurchaseProductsPageDriver(AppVar core)
        {
            Core = new WindowControl(core);
        }
    }

    public static class RequiringPurchaseProductsPageDriverExtensions
    {
        [WindowDriverIdentify(TypeFullName = "AdventureWorks.Business.Purchasing.View.MainWindow")]
        public static RequiringPurchaseProductsPageDriver AttachRequiringPurchaseProductsPage(this WindowsAppFriend app)
            => app.WaitForIdentifyFromTypeFullName("AdventureWorks.Business.Purchasing.View.MainWindow").Dynamic();
    }
}