using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Server;

/// <summary>
/// 要再発注製品に対するクエリーサービス
/// </summary>
/// <remarks>
/// アセンブリーを解析してサービス登録するため、参照している箇所が無いように見えるが実際には利用されているため
/// 警告を抑制する。
/// </remarks>
// ReSharper disable once UnusedMember.Global
public class RequiringPurchaseProductQueryService : ServiceBase<IRequiringPurchaseProductQueryService>, IRequiringPurchaseProductQueryService
{
    /// <summary>
    /// 要再発注製品に対するクエリー
    /// </summary>
    private readonly IRequiringPurchaseProductQuery _service;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="service"></param>
    public RequiringPurchaseProductQueryService(IRequiringPurchaseProductQuery service)
    {
        _service = service;
    }

    /// <summary>
    /// 要再発注製品を取得する
    /// </summary>
    /// <returns></returns>
    public async UnaryResult<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync()
    {
        try
        {
            return await _service.GetRequiringPurchaseProductsAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}