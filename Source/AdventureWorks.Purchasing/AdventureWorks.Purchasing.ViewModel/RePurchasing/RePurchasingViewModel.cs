using System.Collections.ObjectModel;
using AdventureWorks;
using AdventureWorks.Authentication;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using CommunityToolkit.Mvvm.Input;
using Kamishibai;

namespace AdventureWorks.Purchasing.ViewModel.RePurchasing;

[Navigate]
public partial class RePurchasingViewModel : INavigatedAsyncAware
{
    private readonly IPresentationService _presentationService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IShipMethodRepository _shipMethodRepository;

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

    [RelayCommand]
    private Task PurchaseAsync()
    {
        //PurchaseOrderBuilder builder =
        //    new(
        //        _authenticationService.CurrentEmployee.Id,
        //        Vendor,

        //        )
        throw new NotImplementedException();
    }
}

