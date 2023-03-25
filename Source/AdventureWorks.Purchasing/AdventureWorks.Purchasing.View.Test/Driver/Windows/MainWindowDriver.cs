using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;

namespace Driver.Windows
{
    [WindowDriver(TypeFullName = "AdventureWorks.Purchasing.View.MainWindow")]
    public class MainWindowDriver
    {
        public WindowControl Core { get; }
        public NavigationFrameDriver NavigationFrame => Core.LogicalTree().ByType("Kamishibai.NavigationFrame").FirstOrDefault()?.Dynamic();

        public MainWindowDriver(WindowControl core)
        {
            Core = core;
        }

        public MainWindowDriver(AppVar core)
        {
            Core = new WindowControl(core);
        }
    }

    public static class MainWindowDriverExtensions
    {
        [WindowDriverIdentify(TypeFullName = "AdventureWorks.Purchasing.View.MainWindow")]
        public static MainWindowDriver AttachMainWindow(this WindowsAppFriend app)
            => app.WaitForIdentifyFromTypeFullName("AdventureWorks.Purchasing.View.MainWindow").Dynamic();
    }
}