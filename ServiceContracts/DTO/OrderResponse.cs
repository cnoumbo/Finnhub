using System;
using Entities;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
	public class OrderResponse
	{
        public Guid ID { get; set; }
        public string StockSymbol { get; set; }
        public string StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
        public OrderType Type { get; set; }
        public double TradeAmount { get; set; }
    }
}

