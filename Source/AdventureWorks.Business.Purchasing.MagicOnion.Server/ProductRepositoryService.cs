using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Server;

/// <summary>
/// 製品リポジトリーサービス
/// </summary>
/// <remarks>
/// アセンブリーを解析してサービス登録するため、参照している箇所が無いように見えるが実際には利用されているため
/// 警告を抑制する。
/// </remarks>
// ReSharper disable once UnusedMember.Global
public class ProductRepositoryService : ServiceBase<IProductRepositoryService>, IProductRepositoryService
{
    /// <summary>
    /// 製品リポジトリー
    /// </summary>
    private readonly IProductRepository _repository;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="repository"></param>
    public ProductRepositoryService(IProductRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 製品を取得する。
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public async UnaryResult<Product> GetProductByIdAsync(ProductId productId)
    {
        return await _repository.GetProductByIdAsync(productId);
    }
}