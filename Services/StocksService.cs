using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Helpers;
using Entities;

namespace Services
{
    public class StocksService : IStocksService
    {
        private readonly List<BuyOrder> _buyOrders;
        private readonly List<SellOrder> _sellOrders;

        public StocksService()
        {
            _buyOrders = new List<BuyOrder>();
            _sellOrders = new List<SellOrder>();
        }

        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null) 
            {
                throw new ArgumentNullException(nameof(buyOrderRequest));
            }

            ValidationHelper.ModelValidation(buyOrderRequest);
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            buyOrder.BuyOrderID = Guid.NewGuid();
            _buyOrders.Add(buyOrder);
            return buyOrder.ToBuyOrderResponse();
        }

        public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null) 
            {
                throw new ArgumentNullException(nameof(sellOrderRequest));
            }

            ValidationHelper.ModelValidation(sellOrderRequest);
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            sellOrder.SellOrderID = Guid.NewGuid();
            _sellOrders.Add(sellOrder);
            return sellOrder.ToSellOrderResponse();
        }

        public List<BuyOrderResponse> GetAllBuyOrders()
        {
            return _buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();
        }
        public List<SellOrderResponse> GetAllSellOrders()
        {
            return _sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();
        }
    }
}
