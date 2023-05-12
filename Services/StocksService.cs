using ServiceContracts.DTO;
using ServiceContracts;
using Services.Helpers;
using Entities;

namespace Services;

public class StocksService : IStocksService
{
    private readonly List<BuyOrder> _buyOrders;
    private readonly List<SellOrder> _sellOrders;

    /// <summary>
    /// Constructor
    /// </summary>
    public StocksService()
    {
        _buyOrders = new List<BuyOrder>();
        _sellOrders = new List<SellOrder>();
    }

    public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
    {
        // args validation
        if (buyOrderRequest == null)
            throw new ArgumentNullException(nameof(buyOrderRequest));

        // Model Validation
        ValidationHelper.ModelValidation(buyOrderRequest);

        BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

        buyOrder.BuyOrderID = Guid.NewGuid();

        _buyOrders.Add(buyOrder);

        return buyOrder.ToBuyOrderResponse();
    }

    public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
    {
        // args validation
        if (sellOrderRequest == null)
            throw new ArgumentNullException(nameof(SellOrderRequest));

        // Model validation
        ValidationHelper.ModelValidation(sellOrderRequest);

        // Creating process
        SellOrder sellOrder = sellOrderRequest.ToSellOrder();
        sellOrder.SellOrderID = Guid.NewGuid();
        _sellOrders.Add(sellOrder);

        return sellOrder.ToSellOrderResponse();
    }

    public List<BuyOrderResponse> GetBuyOrders()
    {

        return _buyOrders.OrderByDescending(item => item.DateAndTimeOfOrder)
            .Select(item => item.ToBuyOrderResponse()).ToList();
    }

    public List<SellOrderResponse> GetSellOrders()
    {
        return _sellOrders.OrderByDescending(item => item.DateAndTimeOfOrder)
            .Select(item => item.ToSellOrderResponse()).ToList();
    }
}

