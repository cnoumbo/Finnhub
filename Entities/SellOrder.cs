using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
	public class SellOrder
	{
        [Key]
        public Guid SellOrderID { get; set; }

        [Required]
        public string StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock Name can't be null")]
        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        [Required]
        [Range(1, 100000, ErrorMessage = "You can buy maximum of 100000.")]
        public uint Quantity { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Maximum Stock price is 10000")]
        public double Price { get; set; }
    }
}

