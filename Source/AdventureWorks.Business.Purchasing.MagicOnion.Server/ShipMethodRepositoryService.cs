﻿using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Server;

/// <summary>
/// 支払い方法リポジトリーサービス
/// </summary>
/// <remarks>
/// アセンブリーを解析してサービス登録するため、参照している箇所が無いように見えるが実際には利用されているため
/// 警告を抑制する。
/// </remarks>
// ReSharper disable once UnusedMember.Global
public class ShipMethodRepositoryService : ServiceBase<IShipMethodRepositoryService>, IShipMethodRepositoryService
{
    /// <summary>
    /// 支払い方法リポジトリー
    /// </summary>
    private readonly IShipMethodRepository _repository;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="repository"></param>
    public ShipMethodRepositoryService(IShipMethodRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 支払い方法を取得する。
    /// </summary>
    /// <returns></returns>
    public async UnaryResult<IList<ShipMethod>> GetShipMethodsAsync()
    {
        return await _repository.GetShipMethodsAsync();
    }
}