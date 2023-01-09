using AdventureWorks.Purchasing.UseCase.RePurchasing;
using CommunityToolkit.Mvvm.Input;
using Kamishibai;

namespace AdventureWorks.Purchasing.ViewModel.RePurchasing;

[Navigate]
public partial class RePurchasingViewModel : INavigatedAsyncAware
{
    private readonly Vendor _vendor;
    private readonly IList<RequiringPurchaseProduct> _requiringPurchaseProducts;
    private readonly IPresentationService _presentationService;

    public RePurchasingViewModel(
        Vendor vendor,
        IEnumerable<RequiringPurchaseProduct> requiringPurchaseProducts,
        [Inject] IPresentationService presentationService)
    {
        _vendor = vendor;
        _requiringPurchaseProducts = requiringPurchaseProducts.ToList();
        _presentationService = presentationService;
    }

    [RelayCommand]
    public Task GoBackAsync() => _presentationService.GoBackAsync();

    public Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        throw new NotImplementedException();
    }


    [RelayCommand]
    private Task PurchaseAsync()
    {
        throw new NotImplementedException();
    }
}