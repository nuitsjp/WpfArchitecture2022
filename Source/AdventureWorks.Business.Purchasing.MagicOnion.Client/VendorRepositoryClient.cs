using AdventureWorks.MagicOnion.Client;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Client;

/// <summary>
/// ベンダーリポジトリークライアント
/// </summary>
public class VendorRepositoryClient : IVendorRepository
{
    /// <summary>
    /// IMagicOnionClientFactory
    /// </summary>
    private readonly IMagicOnionClientFactory _clientFactory;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="clientFactory"></param>
    public VendorRepositoryClient(IMagicOnionClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    /// <summary>
    /// ベンダーを取得する。
    /// </summary>
    /// <param name="vendorId"></param>
    /// <returns></returns>
    public async Task<Vendor> GetVendorByIdAsync(VendorId vendorId)
    {
        var server = _clientFactory.Create<IVendorRepositoryService>();
        return await server.GetVendorByIdAsync(vendorId);
    }
}