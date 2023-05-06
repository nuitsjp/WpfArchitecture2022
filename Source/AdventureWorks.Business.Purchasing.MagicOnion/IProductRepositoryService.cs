using MagicOnion;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

/// <summary>
/// 製品リポジトリーサービス
/// </summary>
public interface IProductRepositoryService : IService<IProductRepositoryService>
{
    /// <summary>
    /// 製品を取得する。
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    UnaryResult<Product> GetProductByIdAsync(ProductId productId);

}