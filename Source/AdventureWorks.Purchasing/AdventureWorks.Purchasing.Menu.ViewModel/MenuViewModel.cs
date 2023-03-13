using CommunityToolkit.Mvvm.Input;
using Kamishibai;

namespace AdventureWorks.Purchasing.Menu.ViewModel;

[Navigate]
public class MenuViewModel
{
    private readonly RePurchasing.ViewModel.IPresentationService _presentationService;

    public MenuViewModel(
        [Inject] RePurchasing.ViewModel.IPresentationService presentationService)
    {
        _presentationService = presentationService;

        NavigateRePurchasingCommand = new AsyncRelayCommand(NavigateRePurchasingAsync);
    }

    public IAsyncRelayCommand NavigateRePurchasingCommand { get; }

    private Task NavigateRePurchasingAsync() => _presentationService.NavigateToRequiringPurchaseProductsAsync();
}