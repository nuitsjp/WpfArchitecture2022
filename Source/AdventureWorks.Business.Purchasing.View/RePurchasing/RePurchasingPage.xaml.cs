using AdventureWorks.Business.Purchasing.RePurchasing.ViewModel;

namespace AdventureWorks.Business.Purchasing.View.RePurchasing;
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
    public RePurchasingDesignViewModel() : 
        base(
            default!, 
            default!, 
            default!,
            default!,
            default!,
            default!, 
            default!)
    {
    }
}