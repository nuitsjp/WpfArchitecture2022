using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kamishibai;

namespace AdventureWorks.Business.Purchasing.RePurchasing.ViewModel;

/// <summary>
/// 要再発注製品一覧ページViewModel
/// </summary>
[Navigate]
[INotifyPropertyChanged]
public partial class RequiringPurchaseProductsViewModel : 
    INavigatedAsyncAware,
    IResumingAsyncAware
{
    /// <summary>
    /// 要再発注一覧取得クエリー
    /// </summary>
    private readonly IRequiringPurchaseProductQuery _requiringPurchaseProductQuery;
    /// <summary>
    /// ベンダーリポジトリー
    /// </summary>
    private readonly IVendorRepository _vendorRepository;
    /// <summary>
    /// プレゼンテーションサービス
    /// </summary>
    private readonly IPresentationService _presentationService;

    /// <summary>
    /// 選択中の要再発注製品
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(PurchaseCommand))]
    private RequiringPurchaseProduct? _selectedRequiringPurchaseProduct;

    /// <summary>
    /// 要再発注製品一覧
    /// </summary>
    public ObservableCollection<RequiringPurchaseProduct> RequiringPurchaseProducts { get; } = new();

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="requiringPurchaseProductQuery"></param>
    /// <param name="vendorRepository"></param>
    /// <param name="presentationService"></param>
    public RequiringPurchaseProductsViewModel(
        [Inject] IRequiringPurchaseProductQuery requiringPurchaseProductQuery, 
        [Inject] IVendorRepository vendorRepository, 
        [Inject] IPresentationService presentationService)
    {
        _requiringPurchaseProductQuery = requiringPurchaseProductQuery;
        _vendorRepository = vendorRepository;
        _presentationService = presentationService;
    }

    /// <summary>
    /// ナビゲーション完了時の処理
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        // 要再発注製品一覧を取得する。
        RequiringPurchaseProducts.Replace(await _requiringPurchaseProductQuery.GetRequiringPurchaseProductsAsync());
    }

    /// <summary>
    /// 派中完了後に戻ってきたときの処理
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task OnResumingAsync(PreBackwardEventArgs args)
    {
        // 要再発注製品一覧を取得する。
        RequiringPurchaseProducts.Replace(await _requiringPurchaseProductQuery.GetRequiringPurchaseProductsAsync());
    }

    /// <summary>
    /// 戻る
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private Task GoBackAsync() => _presentationService.GoBackAsync();

    /// <summary>
    /// 発注する
    /// </summary>
    /// <returns></returns>
    [RelayCommand(CanExecute = nameof(CanPurchase))]
    private async Task PurchaseAsync()
    {
        var vendor = await _vendorRepository.GetVendorByIdAsync(_selectedRequiringPurchaseProduct!.VendorId);
        var requiringPurchaseProducts = RequiringPurchaseProducts
            .Where(x => x.VendorId == _selectedRequiringPurchaseProduct!.VendorId);

        await _presentationService.NavigateToRePurchasingAsync(vendor, requiringPurchaseProducts);
    }

    /// <summary>
    /// 製品が未選択の場合、発注を不可とする。
    /// </summary>
    /// <returns></returns>
    private bool CanPurchase()
    {
        return _selectedRequiringPurchaseProduct is not null;
    }
}