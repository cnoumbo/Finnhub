﻿using System;
using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO
{
	public class BuyOrderRequest : IValidatableObject
	{
        [Required]
        public string StockSymbol { get; set; }

        [Required]
        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; } = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);

        [Range(1, 100000)]
        public uint Quantity { get; set; }

        [Range(1, 10000)]
        public double Price { get; set; }

        public BuyOrder ToBuyOrder()
        {
            return new BuyOrder()
            {
                StockName = StockName,
                StockSymbol = StockSymbol,
                DateAndTimeOfOrder = DateAndTimeOfOrder,
                Quantity = Quantity,
                Price = Price
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            // Date of order should'nt be less than Jan 01, 2000
            if (DateAndTimeOfOrder < Convert.ToDateTime("2000-01-01"))
                results.Add(new ValidationResult("Order Date can't be less than 2000-01-01"));

            return results;
        }
    }
}

