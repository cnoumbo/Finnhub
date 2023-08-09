using ServiceContracts.DTO;
using ServiceContracts;
using Services.Helpers;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class StocksService : IStocksService
{
    private readonly List<BuyOrder> _buyOrders;
    private readonly List<SellOrder> _sellOrders;
    private readonly StockMarketDbContext _dbContext;

    /// <summary>
    /// Constructor
    /// </summary>
    public StocksService(StockMarketDbContext dbContext)
    {
        _buyOrders = new List<BuyOrder>();
        _sellOrders = new List<SellOrder>();
        _dbContext = dbContext;
    }

    public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
    {
        // args validation
        if (buyOrderRequest == null)
            throw new ArgumentNullException(nameof(buyOrderRequest));

        // Model Validation
        ValidationHelper.ModelValidation(buyOrderRequest);

        BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

        buyOrder.BuyOrderID = Guid.NewGuid();

        //_buyOrders.Add(buyOrder);

        await _dbContext.BuyOrders.AddAsync(buyOrder);
        await _dbContext.SaveChangesAsync();

        return buyOrder.ToBuyOrderResponse();
    }

    public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
    {
        // args validation
        if (sellOrderRequest == null)
            throw new ArgumentNullException(nameof(SellOrderRequest));

        // Model validation
        ValidationHelper.ModelValidation(sellOrderRequest);

        // Creating process
        SellOrder sellOrder = sellOrderRequest.ToSellOrder();
        sellOrder.SellOrderID = Guid.NewGuid();
        //_sellOrders.Add(sellOrder);

        // Add database capability
        await _dbContext.SellOrders.AddAsync(sellOrder);
        await _dbContext.SaveChangesAsync();

        return sellOrder.ToSellOrderResponse();
    }

    public async Task<List<BuyOrderResponse>> GetBuyOrders()
    {

        //return _buyOrders.OrderByDescending(item => item.DateAndTimeOfOrder)
        //    .Select(item => item.ToBuyOrderResponse()).ToList();

        return await _dbContext.BuyOrders
            .Select(item => item.ToBuyOrderResponse())
            .ToListAsync();
    }

    public async Task<List<SellOrderResponse>> GetSellOrders()
    {
        //return _sellOrders.OrderByDescending(item => item.DateAndTimeOfOrder)
        //    .Select(item => item.ToSellOrderResponse()).ToList();
        return await _dbContext.SellOrders
            .Select(item => item.ToSellOrderResponse())
            .ToListAsync();
    }
}

