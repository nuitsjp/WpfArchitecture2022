using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;
using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Driver.Windows
{
    [ControlDriver(TypeFullName = "GrapeCity.Windows.SpreadGrid.GcSpreadGrid", Priority = 2)]
    public class GcSpreadGridDriver : WPFUIElement
    {
        public int RowCount => this.Dynamic().RowCount;
        public AppVar Rows => this.Dynamic().Rows;

        public GcSpreadGridDriver(AppVar appVar)
            : base(appVar) { }
    }
}
