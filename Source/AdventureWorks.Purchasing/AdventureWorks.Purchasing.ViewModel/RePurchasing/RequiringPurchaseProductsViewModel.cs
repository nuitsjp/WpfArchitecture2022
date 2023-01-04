using System.Collections.ObjectModel;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using CommunityToolkit.Mvvm.ComponentModel;
using Kamishibai;

namespace AdventureWorks.Purchasing.ViewModel.RePurchasing;

[Navigate]
[INotifyPropertyChanged]
public partial class RequiringPurchaseProductsViewModel : INavigatedAsyncAware
{
    private readonly IRePurchasingService _rePurchasingService;

    public ObservableCollection<RequiringPurchaseProduct> RequiringPurchaseProducts { get; } = new();

    public RequiringPurchaseProductsViewModel(
        [Inject] IRePurchasingService rePurchasingService)
    {
        _rePurchasingService = rePurchasingService;
    }

    public async Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        RequiringPurchaseProducts.Replace(await _rePurchasingService.GetRequiringPurchaseProductsAsync());
    }
}