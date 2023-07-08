using System.ComponentModel.DataAnnotations;


namespace Entities
{
    /// <summary>
    /// An object of Sell Order
    /// </summary>
    public class SellOrder
    {
        public Guid SellOrderID { get; set; }

        public string? StockSymbol { get; set; }

        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        public uint Quantity { get; set; }

        public double Price { get; set; }
    }
}
