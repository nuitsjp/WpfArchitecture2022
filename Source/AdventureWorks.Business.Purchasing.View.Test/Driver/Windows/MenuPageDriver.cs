using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;

namespace Driver.Windows
{
    [WindowDriver(TypeFullName = "AdventureWorks.Business.Purchasing.View.MainWindow")]
    public class MenuPageDriver
    {
        public WindowControl Core { get; }
        public WPFButtonBase NavigateRePurchasing => Core.LogicalTree().ByType("AdventureWorks.Business.Purchasing.View.Menu.MenuPage").Single().LogicalTree().ByBinding("NavigateRePurchasingCommand").Single().Dynamic().Content; 

        public MenuPageDriver(WindowControl core)
        {
            Core = core;
        }

        public MenuPageDriver(AppVar core)
        {
            Core = new WindowControl(core);
        }
    }

    public static class MenuPageDriverExtensions
    {
        [WindowDriverIdentify(TypeFullName = "AdventureWorks.Business.Purchasing.View.MainWindow")]
        public static MenuPageDriver AttachMenuPage(this WindowsAppFriend app)
            => app.WaitForIdentifyFromTypeFullName("AdventureWorks.Business.Purchasing.View.MainWindow").Dynamic();
    }
}