using System.Collections.ObjectModel;
using AdventureWorks.Authentication;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kamishibai;

namespace AdventureWorks.Purchasing.ViewModel.RePurchasing;

[Navigate]
[INotifyPropertyChanged]
public partial class RePurchasingViewModel : INavigatedAsyncAware
{
    private readonly IPresentationService _presentationService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IShipMethodRepository _shipMethodRepository;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(PurchaseCommand))]
    private ShipMethod? _selectedShipMethod;

    public RePurchasingViewModel(
        Vendor vendor,
        IEnumerable<RequiringPurchaseProduct> requiringPurchaseProducts,
        [Inject] IPresentationService presentationService,
        [Inject] IAuthenticationService authenticationService,
        [Inject] IShipMethodRepository shipMethodRepository)
    {
        Vendor = vendor;
        RequiringPurchaseProducts = requiringPurchaseProducts.ToList();
        _presentationService = presentationService;
        _authenticationService = authenticationService;
        _shipMethodRepository = shipMethodRepository;
        TotalPrice =
            RequiringPurchaseProducts.Sum(x => x.LineTotal)
            * Vendor.TaxRate;
    }

    public Vendor Vendor { get; }
    public IList<RequiringPurchaseProduct> RequiringPurchaseProducts { get; }

    public Dollar TotalPrice { get; }

    public ObservableCollection<ShipMethod> ShipMethods { get; } = new();

    public async Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        ShipMethods.Replace(await _shipMethodRepository.GetShipMethodsAsync());
    }

    [RelayCommand]
    public Task GoBackAsync() => _presentationService.GoBackAsync();

    [RelayCommand(CanExecute = nameof(CanPurchase))]
    private Task PurchaseAsync()
    {
        //PurchaseOrderBuilder builder =
        //    new(
        //        _authenticationService.CurrentEmployee.Id,
        //        Vendor,

        //        )
        throw new NotImplementedException();
    }

    private bool CanPurchase() => _selectedShipMethod is not null;
}

