using AdventureWorks.Purchasing.Menu.ViewModel;

namespace AdventureWorks.Purchasing.View.Menu
{
    /// <summary>
    /// MenuPage.xaml の相互作用ロジック
    /// </summary>
    public partial class MenuPage
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
