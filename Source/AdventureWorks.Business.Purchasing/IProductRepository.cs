namespace AdventureWorks.Business.Purchasing;

/// <summary>
/// 製品リポジトリ
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// 製品を取得する。
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    Task<Product> GetProductByIdAsync(ProductId productId);
}