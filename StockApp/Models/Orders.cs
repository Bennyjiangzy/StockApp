using ServiceContracts;
using ServiceContracts.DTO;

namespace StockApp.Models
{
    public class Orders
    {
        public List<BuyOrderResponse> BuyOrders { get; set; }

        public List<SellOrderResponse> SellOrders { get; set; }
    }
}
