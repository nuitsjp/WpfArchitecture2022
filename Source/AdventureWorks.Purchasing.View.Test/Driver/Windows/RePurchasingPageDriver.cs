using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;
using System.Windows.Controls;

namespace Driver.Windows
{
    [WindowDriver(TypeFullName = "AdventureWorks.Purchasing.View.MainWindow")]
    public class RePurchasingPageDriver
    {
        public WindowControl Core { get; }
        public WPFButtonBase GoBackCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("GoBackCommand").FirstOrDefault()?.Dynamic(); 
        public WPFTextBlock VendorAccountNumber => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("Vendor.AccountNumber").FirstOrDefault()?.Dynamic(); 
        public WPFTextBlock VendorName => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("Vendor.Name").FirstOrDefault()?.Dynamic(); 
        public WPFTextBlock VendorCreditRating => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("Vendor.CreditRating").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton VendorIsPreferredVendor => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("Vendor.IsPreferredVendor").FirstOrDefault()?.Dynamic(); 
        public GcSpreadGridDriver RequiringPurchaseProducts => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("RequiringPurchaseProducts").FirstOrDefault()?.Dynamic(); 
        public WPFTextBlock VendorTaxRate => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("Vendor.TaxRate").FirstOrDefault()?.Dynamic(); 
        public WPFTextBlock TotalPrice => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("TotalPrice").FirstOrDefault()?.Dynamic(); 
        public AppVar SelectedShipMethod => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("SelectedShipMethod").FirstOrDefault()?.Dynamic(); 
        public WPFButtonBase PurchaseCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("PurchaseCommand").FirstOrDefault()?.Dynamic(); 

        public RePurchasingPageDriver(WindowControl core)
        {
            Core = core;
        }

        public RePurchasingPageDriver(AppVar core)
        {
            Core = new WindowControl(core);
        }
    }

    public static class RePurchasingPageDriverExtensions
    {
        [WindowDriverIdentify(TypeFullName = "AdventureWorks.Purchasing.View.MainWindow")]
        public static RePurchasingPageDriver AttachRePurchasingPage(this WindowsAppFriend app)
            => app.WaitForIdentifyFromTypeFullName("AdventureWorks.Purchasing.View.MainWindow").Dynamic();
    }
}