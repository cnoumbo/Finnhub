using ServiceContracts.DTO;

namespace ServiceContracts;

/// <summary>
/// Stock Service Interface with all operations (buying and selling)
/// </summary>
public interface IStocksService
{
    /// <summary>
    /// Create buy order
    /// </summary>
    /// <param name="buyOrderRequest">BuyOrderRequest object</param>
    /// <returns></returns>
    Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sellOrderRequest"></param>
    /// <returns></returns>
    Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<List<BuyOrderResponse>> GetBuyOrders();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<List<SellOrderResponse>> GetSellOrders();
}

