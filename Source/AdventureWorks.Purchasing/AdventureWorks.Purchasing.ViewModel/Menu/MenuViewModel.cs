using CommunityToolkit.Mvvm.Input;
using Kamishibai;

namespace AdventureWorks.Purchasing.ViewModel.Menu;

[Navigate]
public class MenuViewModel
{
    private readonly IPresentationService _presentationService;

    public MenuViewModel(
        [Inject] IPresentationService presentationService)
    {
        _presentationService = presentationService;

        NavigateRePurchasingCommand = new AsyncRelayCommand(NavigateRePurchasingAsync);
    }

    public IAsyncRelayCommand NavigateRePurchasingCommand { get; }

    private Task NavigateRePurchasingAsync() => _presentationService.NavigateToRequiringPurchaseProductsAsync();
}