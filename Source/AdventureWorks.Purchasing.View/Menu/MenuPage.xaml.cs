using AdventureWorks.Business.Purchasing.Menu.ViewModel;

namespace AdventureWorks.Business.Purchasing.View.Menu
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
