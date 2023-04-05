using System.Windows;
using System.Windows.Input;
using MahApps.Metro.IconPacks;

namespace AdventureWorks.Business.Purchasing.View.Menu
{
    /// <summary>
    /// MenuItemButton.xaml の相互作用ロジック
    /// </summary>
    public partial class MenuItemButton
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

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command), typeof(ICommand), typeof(MenuItemButton), new PropertyMetadata(default(ICommand)));

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}
