using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;

namespace Driver.Windows
{
    [ControlDriver(TypeFullName = "GrapeCity.Windows.SpreadGrid.GcSpreadGrid", Priority = 2)]
    public class GcSpreadGridDriver : WPFUIElement
    {
        public int RowCount => this.Dynamic().RowCount;

        public GcSpreadGridDriver(AppVar appVar)
            : base(appVar) { }
    }
}
