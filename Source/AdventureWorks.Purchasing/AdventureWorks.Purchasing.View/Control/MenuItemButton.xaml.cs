using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.IconPacks;

namespace AdventureWorks.Purchasing.View.Control
{
    /// <summary>
    /// MenuItemButton.xaml の相互作用ロジック
    /// </summary>
    public partial class MenuItemButton : UserControl
    {
        public MenuItemButton()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            nameof(Icon), typeof(PackIconFontAwesomeKind), typeof(MenuItemButton), new PropertyMetadata(default(PackIconFontAwesomeKind)));

        public PackIconFontAwesomeKind Icon
        {
            get => (PackIconFontAwesomeKind) GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty MenuLabelProperty = DependencyProperty.Register(
            nameof(MenuLabel), typeof(string), typeof(MenuItemButton), new PropertyMetadata(default(string)));

        public string MenuLabel
        {
            get => (string) GetValue(MenuLabelProperty);
            set => SetValue(MenuLabelProperty, value);
        }
    }
}
