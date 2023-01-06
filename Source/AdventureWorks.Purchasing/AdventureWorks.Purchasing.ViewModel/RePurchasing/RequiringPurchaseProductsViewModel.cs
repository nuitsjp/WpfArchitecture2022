using System.Collections.ObjectModel;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kamishibai;

namespace AdventureWorks.Purchasing.ViewModel.RePurchasing;

[Navigate]
[INotifyPropertyChanged]
public partial class RequiringPurchaseProductsViewModel : INavigatedAsyncAware
{
    private readonly IRePurchasingService _rePurchasingService;
    private readonly IPresentationService _presentationService;

    public ObservableCollection<RequiringPurchaseProduct> RequiringPurchaseProducts { get; } = new();

    public RequiringPurchaseProductsViewModel(
        [Inject] IRePurchasingService rePurchasingService, 
        [Inject] IPresentationService presentationService)
    {
        _rePurchasingService = rePurchasingService;
        _presentationService = presentationService;
    }

    public async Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        RequiringPurchaseProducts.Replace(await _rePurchasingService.GetRequiringPurchaseProductsAsync());
    }

    [RelayCommand]
    private Task GoBackAsync() => _presentationService.GoBackAsync();
}