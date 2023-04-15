using System.Collections.ObjectModel;
using AdventureWorks.Authentication;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kamishibai;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Business.Purchasing.RePurchasing.ViewModel;

[Navigate]
[INotifyPropertyChanged]
public partial class RePurchasingViewModel : INavigatedAsyncAware
{
    private readonly IPresentationService _presentationService;
    private readonly IAuthenticationContext _authenticationContext;
    private readonly IShipMethodRepository _shipMethodRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;
    private readonly ILogger<RePurchasingViewModel> _logger;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(PurchaseCommand))]
    private ShipMethod? _selectedShipMethod;

    public RePurchasingViewModel(
        Vendor vendor,
        IEnumerable<RequiringPurchaseProduct> requiringPurchaseProducts,
        [Inject] IPresentationService presentationService,
        [Inject] IAuthenticationContext authenticationContext,
        [Inject] IShipMethodRepository shipMethodRepository, 
        [Inject] IProductRepository productRepository, 
        [Inject] IPurchaseOrderRepository purchaseOrderRepository, 
        [Inject] ILogger<RePurchasingViewModel> logger)
    {
        Vendor = vendor;
        RequiringPurchaseProducts = requiringPurchaseProducts.ToList();
        _presentationService = presentationService;
        _authenticationContext = authenticationContext;
        _shipMethodRepository = shipMethodRepository;
        _productRepository = productRepository;
        _purchaseOrderRepository = purchaseOrderRepository;
        _logger = logger;
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
        SelectedShipMethod = ShipMethods.First();
    }

    [RelayCommand]
    public Task GoBackAsync() => _presentationService.GoBackAsync();

    [RelayCommand(CanExecute = nameof(CanPurchase))]
    private async Task PurchaseAsync()
    {
        PurchaseOrderBuilder builder =
            new(
                _authenticationContext.CurrentUser.EmployeeId,
                Vendor,
                _selectedShipMethod!, // 未選択の場合コマンドが実行されないため、nullではない。
                Date.Today);

        foreach (var requiringPurchaseProduct in RequiringPurchaseProducts)
        {
            var product = await _productRepository.GetProductByIdAsync(requiringPurchaseProduct.ProductId);
            builder.AddProduct(product, requiringPurchaseProduct.PurchasingQuantity);
        }


        var purchaseOrder = builder.Build();
        await _purchaseOrderRepository.RegisterAsync(purchaseOrder);

        _logger.LogInformation("Ordered to {VendorId}.", purchaseOrder.VendorId);

        _presentationService.ShowMessage(Properties.Resources.RegistrationCompleted);
        await _presentationService.GoBackAsync();
    }

    private bool CanPurchase() => _selectedShipMethod is not null;
}

