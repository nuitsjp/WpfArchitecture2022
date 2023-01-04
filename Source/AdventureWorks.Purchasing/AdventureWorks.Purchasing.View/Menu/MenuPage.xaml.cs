using System.Windows.Controls;
using AdventureWorks.Purchasing.ViewModel.Menu;

namespace AdventureWorks.Purchasing.View.Menu
{
    /// <summary>
    /// MenuPage.xaml の相互作用ロジック
    /// </summary>
    public partial class MenuPage : UserControl
    {
        public MenuPage()
        {
            InitializeComponent();
        }
    }

    public class MenuDesignViewModel : MenuViewModel
    {
        public MenuDesignViewModel() : base(default!)
        {
        }
    }
}
