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
    private readonly IVendorRepository _vendorRepository;
    private readonly IPresentationService _presentationService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(PurchaseCommand))]
    private RequiringPurchaseProduct? _selectedRequiringPurchaseProduct;

    public ObservableCollection<RequiringPurchaseProduct> RequiringPurchaseProducts { get; } = new();

    public RequiringPurchaseProductsViewModel(
        [Inject] IRePurchasingService rePurchasingService, 
        [Inject] IVendorRepository vendorRepository, 
        [Inject] IPresentationService presentationService)
    {
        _rePurchasingService = rePurchasingService;
        _vendorRepository = vendorRepository;
        _presentationService = presentationService;
    }

    public async Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        RequiringPurchaseProducts.Replace(await _rePurchasingService.GetRequiringPurchaseProductsAsync());
    }

    [RelayCommand]
    private Task GoBackAsync() => _presentationService.GoBackAsync();

    [RelayCommand(CanExecute = nameof(CanPurchaseAsync))]
    private async Task PurchaseAsync()
    {
        var vendor = await _vendorRepository.GetVendorByIdAsync(_selectedRequiringPurchaseProduct!.VendorId);
        var requiringPurchaseProducts = RequiringPurchaseProducts
            .Where(x => x.VendorId == _selectedRequiringPurchaseProduct!.VendorId);

        await _presentationService.NavigateToRePurchasingAsync(vendor, requiringPurchaseProducts);
    }

    private bool CanPurchaseAsync()
    {
        return _selectedRequiringPurchaseProduct is not null;
    }
}