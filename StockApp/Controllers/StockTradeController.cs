using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StockApp.Models;
using StockMarketSolution;

namespace StockApp.Controllers
{
        [Route("[controller]")]
        public class StockTradeController : Controller
        {
            private readonly IFinnhubService _finnhubService;
            private readonly IConfiguration _configuration;
            private readonly TradingOptions _tradingOptions;

        public StockTradeController(IFinnhubService finnhubService, IConfiguration configuration, IOptions<TradingOptions> tradingOptions)
        {
            _finnhubService = finnhubService;
            _configuration = configuration;
            _tradingOptions = tradingOptions.Value;
        }

        [Route("/")]
        [Route("[action]")]
        [Route("~/[controller]")]
        public IActionResult Index()
        {
            Dictionary<string, object>? companyProfileDictionary = _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);
            Dictionary<string, object>? stockQuoteDictionary = _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);
            StockTrade stockTrade = new StockTrade() { StockSymbol = _tradingOptions.DefaultStockSymbol };
            stockTrade = new StockTrade() 
            { StockSymbol = Convert.ToString(companyProfileDictionary["ticker"]), 
                StockName = Convert.ToString(companyProfileDictionary["name"]), 
                Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString()) };
            ViewBag.FinnhubToken = _configuration["FinnhubToken"];
            return View(stockTrade);
        }
    }
}
