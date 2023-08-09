using System;
using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO
{
    public class BuyOrderResponse
    {
        public Guid BuyOrderID { get; set; }
        [Required]
        public string? StockSymbol { get; set; }
        [Required]
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        [Range(1, 100000)]
        public uint Quantity { get; set; }
        [Range(1, 10000)]
        public double Price { get; set; }
        public double TradeAmount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is not BuyOrderResponse)
                return false;

            // Convert obj to BuyOrderResponse object
            BuyOrderResponse buyOrderResponse = (BuyOrderResponse)obj;
            return buyOrderResponse.BuyOrderID == BuyOrderID && buyOrderResponse.StockSymbol == StockSymbol && buyOrderResponse.StockName == StockName && buyOrderResponse.DateAndTimeOfOrder == DateAndTimeOfOrder && buyOrderResponse.Quantity == Quantity && buyOrderResponse.Price == Price && buyOrderResponse.TradeAmount == TradeAmount;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Buy Order ID: {BuyOrderID}, Stock Symbol: {StockSymbol}, Stock Name: {StockName}, Date and Time of Buy Order: {DateAndTimeOfOrder.ToString("dd MMM yyyy hh:mm ss tt")}, Quantity: {Quantity}, Buy Price: {Price}, Trade Amount: {TradeAmount}";
        }

        public OrderResponse ToOrderResponse()
        {
            return new OrderResponse() { ID = this.BuyOrderID, StockSymbol = this.StockSymbol, StockName = this.StockName, Price = this.Price, DateAndTimeOfOrder = this.DateAndTimeOfOrder, Quantity = this.Quantity, TradeAmount = this.Price * this.Quantity, Type = OrderType.BuyOrder };
        }
    }

    public static class BuyOrderExtensions
    {
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse() { BuyOrderID = buyOrder.BuyOrderID, StockSymbol = buyOrder.StockSymbol, StockName = buyOrder.StockName, Price = buyOrder.Price, DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder, Quantity = buyOrder.Quantity, TradeAmount = buyOrder.Price * buyOrder.Quantity };
        }
    }
}
