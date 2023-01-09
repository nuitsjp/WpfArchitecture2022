using AdventureWorks.Purchasing.ViewModel.RePurchasing;
using System;
using System.Collections.Generic;
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
using AdventureWorks.Purchasing.UseCase.RePurchasing;

namespace AdventureWorks.Purchasing.View.RePurchasing
{
    /// <summary>
    /// RePurchasingPage.xaml の相互作用ロジック
    /// </summary>
    public partial class RePurchasingPage
    {
        public RePurchasingPage()
        {
            InitializeComponent();
        }
    }

    public class RePurchasingDesignViewModel : RePurchasingViewModel
    {
        public RePurchasingDesignViewModel() : base(default!, default!, default!)
        {
        }
    }
}
