using AdventureWorks.Purchasing.UseCase.RePurchasing;
using Kamishibai;

namespace AdventureWorks.Purchasing.ViewModel.RePurchasing;

[Navigate]
public class RequiringPurchaseProductsViewModel : INavigatedAsyncAware
{
    private readonly IRePurchasingService _rePurchasingService;

    public RequiringPurchaseProductsViewModel(
        [Inject] IRePurchasingService rePurchasingService)
    {
        _rePurchasingService = rePurchasingService;
    }

    public Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        return _rePurchasingService.GetRequiringPurchaseProductsAsync();
    }
}