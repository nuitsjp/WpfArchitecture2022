using System.Collections.ObjectModel;
using AdventureWorks.Authentication;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kamishibai;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Business.Purchasing.RePurchasing.ViewModel;

/// <summary>
/// 再発注ページViewModel
/// </summary>
[Navigate]
[INotifyPropertyChanged]
public partial class RePurchasingViewModel : INavigatedAsyncAware
{
    /// <summary>
    /// プレゼンテーションサービス
    /// </summary>
    private readonly IPresentationService _presentationService;
    /// <summary>
    /// 認証コンテキスト
    /// </summary>
    private readonly IAuthenticationContext _authenticationContext;
    /// <summary>
    /// 配送方法リポジトリ
    /// </summary>
    private readonly IShipMethodRepository _shipMethodRepository;
    /// <summary>
    /// 製品リポジトリ
    /// </summary>
    private readonly IProductRepository _productRepository;
    /// <summary>
    /// 発注リポジトリ
    /// </summary>
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<RePurchasingViewModel> _logger;

    /// <summary>
    /// 選択済み支払い方法
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(PurchaseCommand))]
    private ShipMethod? _selectedShipMethod;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="vendor"></param>
    /// <param name="requiringPurchaseProducts"></param>
    /// <param name="presentationService"></param>
    /// <param name="authenticationContext"></param>
    /// <param name="shipMethodRepository"></param>
    /// <param name="productRepository"></param>
    /// <param name="purchaseOrderRepository"></param>
    /// <param name="logger"></param>
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

    /// <summary>
    /// ベンダー
    /// </summary>
    public Vendor Vendor { get; }
    /// <summary>
    /// 要再発注製品一覧
    /// </summary>
    public IList<RequiringPurchaseProduct> RequiringPurchaseProducts { get; }
    /// <summary>
    /// 総額
    /// </summary>
    public Dollar TotalPrice { get; }
    /// <summary>
    /// 選択可能な支払い方法
    /// </summary>
    public ObservableCollection<ShipMethod> ShipMethods { get; } = new();

    /// <summary>
    /// 画面遷移時の処理
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        // 画面遷移時に支払い方法を取得する。
        ShipMethods.Replace(await _shipMethodRepository.GetShipMethodsAsync());
        SelectedShipMethod = ShipMethods.First();
    }

    /// <summary>
    /// 戻る
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    public Task GoBackAsync() => _presentationService.GoBackAsync();

    /// <summary>
    /// 発注する
    /// </summary>
    /// <returns></returns>
    [RelayCommand(CanExecute = nameof(CanPurchase))]
    private async Task PurchaseAsync()
    {
        // 発注オブジェクトを構築する。
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

        // 発注する。
        await _purchaseOrderRepository.RegisterAsync(purchaseOrder);

        _logger.LogInformation("Ordered to {VendorId}.", purchaseOrder.VendorId);

        // 完了メッセージを表示して、戻る。
        _presentationService.ShowMessage(Purchasing.ViewModel.Properties.Resources.RegistrationCompleted);
        await _presentationService.GoBackAsync();
    }

    /// <summary>
    /// 支払い方法が未選択であれば、発注は不可とする。
    /// </summary>
    /// <returns></returns>
    private bool CanPurchase() => _selectedShipMethod is not null;
}

