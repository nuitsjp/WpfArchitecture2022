using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kamishibai;

namespace AdventureWorks.Business.Purchasing.RePurchasing.ViewModel;

[Navigate]
[INotifyPropertyChanged]
public partial class RequiringPurchaseProductsViewModel : 
    INavigatedAsyncAware,
    IResumingAsyncAware
{
    private readonly IRePurchasingQuery _rePurchasingQuery;
    private readonly IVendorRepository _vendorRepository;
    private readonly IPresentationService _presentationService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(PurchaseCommand))]
    private RequiringPurchaseProduct? _selectedRequiringPurchaseProduct;

    public ObservableCollection<RequiringPurchaseProduct> RequiringPurchaseProducts { get; } = new();

    public RequiringPurchaseProductsViewModel(
        [Inject] IRePurchasingQuery rePurchasingQuery, 
        [Inject] IVendorRepository vendorRepository, 
        [Inject] IPresentationService presentationService)
    {
        _rePurchasingQuery = rePurchasingQuery;
        _vendorRepository = vendorRepository;
        _presentationService = presentationService;
    }

    public async Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        RequiringPurchaseProducts.Replace(await _rePurchasingQuery.GetRequiringPurchaseProductsAsync());
    }

    public async Task OnResumingAsync(PreBackwardEventArgs args)
    {
        RequiringPurchaseProducts.Replace(await _rePurchasingQuery.GetRequiringPurchaseProductsAsync());
    }

    [RelayCommand]
    private Task GoBackAsync() => _presentationService.GoBackAsync();

    [RelayCommand(CanExecute = nameof(CanPurchase))]
    private async Task PurchaseAsync()
    {
        var vendor = await _vendorRepository.GetVendorByIdAsync(_selectedRequiringPurchaseProduct!.VendorId);
        var requiringPurchaseProducts = RequiringPurchaseProducts
            .Where(x => x.VendorId == _selectedRequiringPurchaseProduct!.VendorId);

        await _presentationService.NavigateToRePurchasingAsync(vendor, requiringPurchaseProducts);
    }

    private bool CanPurchase()
    {
        return _selectedRequiringPurchaseProduct is not null;
    }
}