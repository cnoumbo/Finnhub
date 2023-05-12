using System;
using System.ComponentModel.DataAnnotations;
using ServiceContracts.DTO;

namespace FinnhubApp.Models
{
	public class BuyOrder
	{
		public Guid BuyOrderID { get; set; }
		[Required]
		public string? StockSymbol { get; set; }
		[Required]
		public string? StockName { get; set; }
		public DateTime DateAndTimeOfOrder { get; set; }
		[Range(1, 100000)]
		public uint Quatity { get; set; }
		[Range(1, 10000)]
		public double Price { get; set; }

		public BuyOrderResponse ToBuyOrderResponse()
		{
			return new BuyOrderResponse()
			{
				BuyOrderID = this.BuyOrderID,
				StockSymbol = this.StockSymbol,
				StockName = this.StockName,
				DateAndTimeOfOrder = this.DateAndTimeOfOrder,
				Price = this.Price,
				Quantity = this.Quatity
			};
		}
	}
}

