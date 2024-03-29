using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using FinnhubApp.Models;
using FinnhubApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using Rotativa.AspNetCore;

namespace FinnhubApp.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly IOptions<TradingOptions> _options;
        private readonly IFinnhubService _finnhubService;
        private readonly IStocksService _stockService;

        // Constructor
        public TradeController(IOptions<TradingOptions> options, IFinnhubService finnhubService, IStocksService stocksService)
        {
            _options = options;
            _finnhubService = finnhubService;
            _stockService = stocksService;
        }

        [Route("/")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            // retrieve default stock symbol
            if (_options.Value.DefaultStockSymbol == null)
                _options.Value.DefaultStockSymbol = "MSFT";
            if(_options.Value.DefautlOrderQuantity == null)
                _options.Value.DefautlOrderQuantity = 100;

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

            ViewBag.CurrentOrderQuantity =  _options.Value.DefautlOrderQuantity;
            
            return View(stock);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Orders()
        {
            ViewBag.SellOrders = await _stockService.GetSellOrders();
            ViewBag.BuyOrders = await _stockService.GetBuyOrders();

            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrder)
        {
            //buyOrder.DateAndTimeOfOrder = DateTime.Now;

            // Call required service to save the operation
            try {
                await _stockService.CreateBuyOrder(buyOrder);
                return RedirectToAction("Orders", "Trade");
            }
            catch (Exception ex) {
                ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage).ToList(); 
                ViewBag.CurrentOrder = buyOrder;
                ViewBag.CurrentOrderQuantity = buyOrder.Quantity;
                // ViewBag.Errors.Add(ex.Message);

                // Get View Model 
                // retrieve default stock symbol
                if (_options.Value.DefaultStockSymbol == null)
                    _options.Value.DefaultStockSymbol = "MSFT";
                if(_options.Value.DefautlOrderQuantity == null)
                    _options.Value.DefautlOrderQuantity = 100;

                Dictionary<string, object>? companyProfile = await _finnhubService.GetCompanyProfile(_options.Value.DefaultStockSymbol);
                Dictionary<string, object>? stockPriceQuote = await _finnhubService.GetStockPriceQuote(_options.Value.DefaultStockSymbol);

                // Build stock object
                StockTrade stock = new()
                {
                    StockName = companyProfile["name"].ToString(),
                    StockSymbol = companyProfile["ticker"].ToString(),
                    Price = Convert.ToDouble(stockPriceQuote["c"].ToString()),
                    Quantity = Convert.ToUInt32(stockPriceQuote["t"].ToString())
                };

                return View("Index", stock);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SellOrder(SellOrderRequest sellOrder)
        {
            //sellOrder.DateAndTimeOfOrder = DateTime.Now;

            // Call required service to save the operation
            try {
                await _stockService.CreateSellOrder(sellOrder);
                return RedirectToAction("Orders", "Trade");
            }
            catch (Exception ex) {
                ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage).ToList(); 
                ViewBag.CurrentOrder = sellOrder;
                ViewBag.CurrentOrderQuantity = sellOrder.Quantity;
                // ViewBag.Errors.Add(ex.Message);

                // Get View Model 
                // retrieve default stock symbol
                if (_options.Value.DefaultStockSymbol == null)
                    _options.Value.DefaultStockSymbol = "MSFT";
                if(_options.Value.DefautlOrderQuantity == null)
                    _options.Value.DefautlOrderQuantity = 100;

                Dictionary<string, object>? companyProfile = await _finnhubService.GetCompanyProfile(_options.Value.DefaultStockSymbol);
                Dictionary<string, object>? stockPriceQuote = await _finnhubService.GetStockPriceQuote(_options.Value.DefaultStockSymbol);

                // Build stock object
                StockTrade stock = new()
                {
                    StockName = companyProfile["name"].ToString(),
                    StockSymbol = companyProfile["ticker"].ToString(),
                    Price = Convert.ToDouble(stockPriceQuote["c"].ToString()),
                    Quantity = Convert.ToUInt32(stockPriceQuote["t"].ToString())
                };

                return View("Index", stock);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> OrdersPDF()
        {
            // Call required services
            var sellOrders = await _stockService.GetSellOrders();
            var buyOrders = await _stockService.GetBuyOrders();

            // order list
            List<OrderResponse> orders = new List<OrderResponse>();
            foreach(var item in sellOrders)
            {
                orders.Add(item.ToOrderResponse());
            }
            foreach (var item in buyOrders)
            {
                orders.Add(item.ToOrderResponse());
            }

            var orderedOrders = orders.OrderByDescending(item => item.DateAndTimeOfOrder).ToList();

            return new ViewAsPdf("OrdersPDF", orderedOrders, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Bottom = 20, Left = 20, Right = 20 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }
    }
}