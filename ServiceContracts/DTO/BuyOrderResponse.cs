using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class BuyOrderResponse
    {
        public Guid BuyOrderID { get; set; }

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
            if (obj == null || !(obj is BuyOrderResponse other)) return false;

            BuyOrderResponse buyOrder = (BuyOrderResponse)obj;

            return BuyOrderID == buyOrder.BuyOrderID &&
                   StockSymbol == buyOrder.StockSymbol &&
                   StockName == buyOrder.StockName &&
                   DateAndTimeOfOrder == buyOrder.DateAndTimeOfOrder &&
                   Quantity == buyOrder.Quantity &&
                   Price == buyOrder.Price &&
                   TradeAmount == buyOrder.TradeAmount;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"BuyOrder ID: {BuyOrderID}, Stock Symbol: {StockSymbol}, " +
                $"Stock Name: {StockName}, Order Date: {DateAndTimeOfOrder}, " +
                $"Quantity: {Quantity}, Price: {Price}, TradeAmount: {TradeAmount}";
        }
    }

    public static class BuyOrderExtensions 
    {
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder) 
        {
            return new BuyOrderResponse()
            {
                BuyOrderID = buyOrder.BuyOrderID,
                StockSymbol = buyOrder.StockSymbol,
                StockName = buyOrder.StockName,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                Quantity = buyOrder.Quantity,
                Price = buyOrder.Price,
                TradeAmount = buyOrder.Quantity * buyOrder.Price
            };
        }
    }
}
