using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AdventureWorks.Purchasing.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var themes = new C1.WPF.Theming.Office2016.C1ThemeOffice2016White();
            var rd = themes.GetNewResourceDictionary();
            Resources.MergedDictionaries.Add(rd);
        }
    }
}
