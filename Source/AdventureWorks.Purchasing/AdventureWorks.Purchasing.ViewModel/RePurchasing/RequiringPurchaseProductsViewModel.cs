using System.Collections.ObjectModel;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kamishibai;

namespace AdventureWorks.Purchasing.ViewModel.RePurchasing;

[Navigate]
[INotifyPropertyChanged]
public partial class RequiringPurchaseProductsViewModel : 
    INavigatedAsyncAware,
    IResumingAsyncAware
{
    private readonly IRePurchasingQueryService _rePurchasingQueryService;
    private readonly IVendorRepository _vendorRepository;
    private readonly IPresentationService _presentationService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(PurchaseCommand))]
    private RequiringPurchaseProduct? _selectedRequiringPurchaseProduct;

    public ObservableCollection<RequiringPurchaseProduct> RequiringPurchaseProducts { get; } = new();

    public RequiringPurchaseProductsViewModel(
        [Inject] IRePurchasingQueryService rePurchasingQueryService, 
        [Inject] IVendorRepository vendorRepository, 
        [Inject] IPresentationService presentationService)
    {
        _rePurchasingQueryService = rePurchasingQueryService;
        _vendorRepository = vendorRepository;
        _presentationService = presentationService;
    }

    public async Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        RequiringPurchaseProducts.Replace(await _rePurchasingQueryService.GetRequiringPurchaseProductsAsync());
    }

    public async Task OnResumingAsync(PreBackwardEventArgs args)
    {
        RequiringPurchaseProducts.Replace(await _rePurchasingQueryService.GetRequiringPurchaseProductsAsync());
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