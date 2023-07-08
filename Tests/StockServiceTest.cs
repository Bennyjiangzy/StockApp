using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using Xunit.Abstractions;

namespace Tests
{
    public class StockServiceTest
    {
        private readonly IStocksService _stocksService;
        private readonly ITestOutputHelper _testOutputHelper;

        public StockServiceTest(ITestOutputHelper testOutputHelper)
        {
            _stocksService = new StocksService();
            _testOutputHelper = testOutputHelper;
        }


        #region CreateBuyOrder

        [Fact]
        public void CreateBuyOrder_NullBuyOrder_ToBeArgumentNullException()
        {
            BuyOrderRequest? buyOrderRequest = null;

            Assert.Throws<ArgumentNullException>(() => {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });

        }

        [Theory] 
        [InlineData(0)] 
        public void CreateBuyOrder_QuantityIsLessThanMinimum_ToBeArgumentException(uint buyOrderQuantity)
        {
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() 
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Price = 1,
                Quantity = buyOrderQuantity
            };

            Assert.Throws<ArgumentException>(() => 
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Theory] 
        [InlineData(100001)] 
        public void CreateBuyOrder_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint buyOrderQuantity)
        {
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Price = 1,
                Quantity = buyOrderQuantity
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Theory]
        [InlineData(0)]
        public void CreateBuyOrder_PriceIsLessThanMinimum_ToBeArgumentException(uint buyOrderPrice)
        {
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Price = buyOrderPrice,
                Quantity = 10
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Theory]
        [InlineData(10001)]
        public void CreateBuyOrder_PriceIsGreaterThanMaximum_ToBeArgumentException(uint buyOrderPrice)
        {
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Price = buyOrderPrice,
                Quantity = 10
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public void CreateBuyOrder_StockSymbolIsNull_ToBeArgumentException()
        {
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = null,
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Price = 10,
                Quantity = 10
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public void CreateBuyOrder_DateOfOrderIsLessThanYear2000_ToBeArgumentException()
        {
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("1999-01-01"),
                Price = 10,
                Quantity = 10
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public void CreateBuyOrder_ValidData_ToBeSuccessful() 
        {

            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Price = 10,
                Quantity = 10
            };

            BuyOrderResponse buyOrderResponse = _stocksService.CreateBuyOrder(buyOrderRequest);
            Assert.True(buyOrderResponse.BuyOrderID != Guid.Empty);

        }

        #endregion

        #region CreateSellOrder

        [Fact]
        public void CreateSellOrder_NullSellOrder_ToBeArgumentNullException() 
        {
            SellOrderRequest? sellOrderRequest = null;

            Assert.Throws<ArgumentNullException>(() => 
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Theory] 
        [InlineData(0)] 
        public void CreateSellOrder_QuantityIsLessThanMinimum_ToBeArgumentException(uint sellOrderQuantity) 
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Price = 1,
                Quantity = sellOrderQuantity
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Theory] 
        [InlineData(100001)] 
        public void CreateSellOrder_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint sellOrderQuantity) 
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Price = 1,
                Quantity = sellOrderQuantity
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Theory] 
        [InlineData(0)] 
        public void CreateSellOrder_PriceIsLessThanMinimum_ToBeArgumentException(uint sellOrderPrice) 
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Price = sellOrderPrice,
                Quantity = 10
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Theory]
        [InlineData(10001)]
        public void CreateSellOrder_PriceIsGreaterThanMaximum_ToBeArgumentException(uint sellOrderPrice) 
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Price = sellOrderPrice,
                Quantity = 10
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public void CreateSellOrder_StockSymbolIsNull_ToBeArgumentException() 
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = null,
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Price = 10,
                Quantity = 10
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public void CreateSellOrder_DateOfOrderIsLessThanYear2000_ToBeArgumentException() 
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("1999-01-01"),
                Price = 10,
                Quantity = 10
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public void CreateSellOrder_ValidData_ToBeSuccessful() 
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Price = 10,
                Quantity = 10
            };

            SellOrderResponse sellOrderResponse = _stocksService.CreateSellOrder(sellOrderRequest);
            Assert.True(sellOrderResponse.SellOrderID != Guid.Empty);
        }

        #endregion

        #region GetBuyOrders

        [Fact]
        public void GetAllBuyOrders_DefaultList_ToBeEmpty() 
        {
            List<BuyOrderResponse> buyOrderResponsesList = _stocksService.GetAllBuyOrders();
            Assert.Empty(buyOrderResponsesList);
        }

        [Fact]
        public void GetAllBuyOrders_WithFewBuyOrders_ToBeSuccessful() 
        {
            BuyOrderRequest? buyOrderRequest1 = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Price = 10,
                Quantity = 10
            };

            BuyOrderRequest? buyOrderRequest2 = new BuyOrderRequest()
            {
                StockSymbol = "TEST",
                StockName = "TEESSSTTT",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Price = 20,
                Quantity = 10
            };
            List<BuyOrderRequest> TestcaseRequest = new List<BuyOrderRequest>()
            {
                buyOrderRequest1,
                buyOrderRequest2
            };
            List<BuyOrderResponse> Response_After_Add = new List<BuyOrderResponse>();
            foreach (BuyOrderRequest Request in TestcaseRequest) 
            {
                BuyOrderResponse Response = _stocksService.CreateBuyOrder(Request);
                Response_After_Add.Add(Response);
            };

            //Print the case
            _testOutputHelper.WriteLine("Expected:\n");
            foreach (BuyOrderResponse response in Response_After_Add) 
            {
                _testOutputHelper.WriteLine(response.ToString());
            };

            //ACT
            List<BuyOrderResponse> buyOrderResponsesList = _stocksService.GetAllBuyOrders();

            //Print the case
            _testOutputHelper.WriteLine("Actual:\n");
            foreach (BuyOrderResponse response in buyOrderResponsesList)
            {
                _testOutputHelper.WriteLine(response.ToString());
            };

            foreach (BuyOrderResponse response in buyOrderResponsesList) 
            {
                Assert.Contains(response, Response_After_Add);
            }



        }
        #endregion

        #region GetSellOrders
        [Fact]
        public void GetAllSellOrders_DefaultList_ToBeEmpty() 
        { 
            List<SellOrderResponse> sellOrderResponses = _stocksService.GetAllSellOrders();

            Assert.Empty(sellOrderResponses);
        }

        [Fact]
        public void GetAllSellOrders_WithFewSellOrders_ToBeSuccessful() 
        {
            SellOrderRequest? sellOrderRequest1 = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Price = 10,
                Quantity = 10
            };

            SellOrderRequest? sellOrderRequest2 = new SellOrderRequest()
            {
                StockSymbol = "TEST",
                StockName = "EEESSTTT",
                DateAndTimeOfOrder = DateTime.Parse("2001-01-01"),
                Price = 20,
                Quantity = 10
            };

            List<SellOrderRequest> TestCase = new List<SellOrderRequest>() {  sellOrderRequest1, sellOrderRequest2 };
            List<SellOrderResponse> Reponse_After_Add = new List<SellOrderResponse>();
            foreach (SellOrderRequest request in TestCase) 
            {
                SellOrderResponse response = _stocksService.CreateSellOrder(request);
                Reponse_After_Add.Add(response);
            };

            //Print the case
            _testOutputHelper.WriteLine("Expected:\n");
            foreach (SellOrderResponse response in Reponse_After_Add) 
            {
               _testOutputHelper.WriteLine(response.ToString());
            };

            //Act
            List<SellOrderResponse> Response_From_call = _stocksService.GetAllSellOrders();

            //Print the case
            _testOutputHelper.WriteLine("Actual:\n");
            foreach (SellOrderResponse response in Response_From_call)
            {
                _testOutputHelper.WriteLine(response.ToString());
            };

            foreach (SellOrderResponse response in Response_From_call)
            {
                Assert.Contains(response, Reponse_After_Add);
            };



        }


        #endregion
    }
}