using CommunityToolkit.Mvvm.Input;
using Kamishibai;

namespace AdventureWorks.Business.Purchasing.Menu.ViewModel;

/// <summary>
/// メニュー画面のViewModel
/// </summary>
[Navigate]
public partial class MenuViewModel
{
    /// <summary>
    /// プレゼンテーションサービス
    /// </summary>
    private readonly RePurchasing.ViewModel.IPresentationService _presentationService;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="presentationService"></param>
    public MenuViewModel(
        [Inject] RePurchasing.ViewModel.IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    /// <summary>
    /// 再発注画面に遷移する。
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private Task NavigateRePurchasingAsync() => _presentationService.NavigateToRequiringPurchaseProductsAsync();
}