using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// An object of BuyOrder
    /// </summary>
    public class BuyOrder
    {
        public Guid BuyOrderID { get; set; }

        public string? StockSymbol { get; set; }

        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        public uint Quantity { get; set; }

        public double Price { get; set; }
    }
}