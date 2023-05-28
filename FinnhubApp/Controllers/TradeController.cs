using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinnhubApp.Models;
using FinnhubApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;

namespace FinnhubApp.Controllers
{
    public class TradeController : Controller
    {
        private readonly IOptions<TradingOptions> _options;
        private readonly IFinnhubService _finnhubService;
        public TradeController(IOptions<TradingOptions> options, IFinnhubService finnhubService)
        {
            _options = options;
            _finnhubService = finnhubService;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            // retrieve default stock symbol
            if (_options.Value.DefaultStockSymbol == null)
                _options.Value.DefaultStockSymbol = "MSFT";

            Dictionary<string, object>? companyProfile = await _finnhubService.GetCompanyProfile(_options.Value.DefaultStockSymbol);
            Dictionary<string, object>? stockPriceQuote = await _finnhubService.GetStockPriceQuote(_options.Value.DefaultStockSymbol);

            // Build stock object
            StockTrade stock = new StockTrade()
            {
                StockName = companyProfile["name"].ToString(),
                StockSymbol = companyProfile["ticker"].ToString(),
                Price = Convert.ToDouble(stockPriceQuote["c"].ToString()),
                Quantity = Convert.ToUInt32(stockPriceQuote["t"].ToString())
            };
            
            return View(stock);
        }
    }
}