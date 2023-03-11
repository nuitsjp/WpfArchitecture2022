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
    [ControlDriver(TypeFullName = "C1.WPF.C1DropDown", Priority = 2)]
    public class C1DropDownDriver : WPFUIElement
    {
        public C1DropDownDriver(AppVar appVar)
            : base(appVar) { }
    }
}
