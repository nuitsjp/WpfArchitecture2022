using CommunityToolkit.Mvvm.Input;
using Kamishibai;

namespace AdventureWorks.Business.Purchasing.Menu.ViewModel;

[Navigate]
public partial class MenuViewModel
{
    private readonly RePurchasing.ViewModel.IPresentationService _presentationService;

    public MenuViewModel(
        [Inject] RePurchasing.ViewModel.IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    [RelayCommand]
    private Task NavigateRePurchasingAsync() => _presentationService.NavigateToRequiringPurchaseProductsAsync();
}