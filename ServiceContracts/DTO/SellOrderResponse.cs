using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }

        public string? StockSymbol { get; set; }

        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        public uint Quantity { get; set; }

        public double Price { get; set; }

        public double TradeAmount { get; set; }

        /// <summary>
        /// Default equalt will only compare address for object, we need to write a custom rules for comparing
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>return true of false based on the matched details</returns>
        public override bool Equals(object? obj)
        {
            if( obj == null || !(obj is SellOrderResponse other)) return false;

            SellOrderResponse sellOrder = (SellOrderResponse)obj;

            return SellOrderID == sellOrder.SellOrderID &&
                   StockSymbol == sellOrder.StockSymbol &&
                   StockName == sellOrder.StockName &&
                   DateAndTimeOfOrder == sellOrder.DateAndTimeOfOrder &&
                   Quantity == sellOrder.Quantity &&
                   Price == sellOrder.Price &&
                   TradeAmount == sellOrder.TradeAmount;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"SellOrder ID: {SellOrderID}, Stock Symbol: {StockSymbol}, " +
                $"Stock Name: {StockName}, Order Date: {DateAndTimeOfOrder}, " +
                $"Quantity: {Quantity}, Price: {Price}, TradeAmount: {TradeAmount}";
        }
    }

    public static class SellOrderExtensions 
    {
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder) 
        {
            return new SellOrderResponse() 
            {
                SellOrderID = sellOrder.SellOrderID,
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Quantity = sellOrder.Quantity,
                Price = sellOrder.Price,
                TradeAmount = sellOrder.Quantity * sellOrder.Price
            };
        }
    }
}
