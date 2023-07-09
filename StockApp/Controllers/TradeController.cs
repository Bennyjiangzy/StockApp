using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.DTO;
using StockApp.Models;
using StockMarketSolution;

namespace StockApp.Controllers
{
        [Route("[controller]")]
        public class TradeController : Controller
        {
            private readonly IFinnhubService _finnhubService;
            private readonly IConfiguration _configuration;
            private readonly TradingOptions _tradingOptions;
            private readonly IStocksService _stocksService;

        public TradeController(IFinnhubService finnhubService, IConfiguration configuration, IOptions<TradingOptions> tradingOptions, IStocksService stocksService)
        {
            _finnhubService = finnhubService;
            _configuration = configuration;
            _tradingOptions = tradingOptions.Value;
            _stocksService = stocksService;
        }

        [Route("/")]
        [Route("[action]")]
        [Route("~/[controller]")]
        public IActionResult Index()
        {
            //reset stock symbol if not exists
            if (string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol))
            { _tradingOptions.DefaultStockSymbol = "MSFT"; }

            //get company profile from API server
            Dictionary<string, object>? companyProfileDictionary = _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);
            
            //get stock price quotes from API server
            Dictionary<string, object>? stockQuoteDictionary = _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);
            
            StockTrade stockTrade = new StockTrade() { StockSymbol = _tradingOptions.DefaultStockSymbol };
            
            stockTrade = new StockTrade() 
            { StockSymbol = Convert.ToString(companyProfileDictionary["ticker"]), 
                StockName = Convert.ToString(companyProfileDictionary["name"]), 
                Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString()) };
            ViewBag.FinnhubToken = _configuration["FinnhubToken"];
            return View(stockTrade);
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult BuyOrder(BuyOrderRequest buyOrderRequest) 
        {
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            //re-validate the model object after updating the date
            ModelState.Clear();
            TryValidateModel(buyOrderRequest);

            if (!ModelState.IsValid) 
            {
                ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade() { StockName = buyOrderRequest.StockName, Quantity = Convert.ToUInt16(buyOrderRequest.Quantity), StockSymbol = buyOrderRequest.StockSymbol };
                return View("Index", stockTrade);
            }
            _stocksService.CreateBuyOrder(buyOrderRequest);
            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult SellOrder(SellOrderRequest sellOrderRequest)
        {
            //update date of order
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            //re-validate the model object after updating the date
            ModelState.Clear();
            TryValidateModel(sellOrderRequest);

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade() { StockName = sellOrderRequest.StockName, Quantity = Convert.ToUInt16(sellOrderRequest.Quantity), StockSymbol = sellOrderRequest.StockSymbol };
                return View("Index", stockTrade);
            }
            _stocksService.CreateSellOrder(sellOrderRequest);
            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        public IActionResult Orders()
        {
            //invoke service methods
            List<BuyOrderResponse> buyOrderResponses = _stocksService.GetAllBuyOrders();
            List<SellOrderResponse> sellOrderResponses = _stocksService.GetAllSellOrders();

            Orders orders = new Orders() { BuyOrders = buyOrderResponses, SellOrders = sellOrderResponses };

            ViewBag.TradingOptions = _tradingOptions;

            return View(orders);
        }
    }
}
