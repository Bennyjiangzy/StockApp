using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using Entities;


namespace ServiceContracts.DTO
{
    public class BuyOrderRequest: IValidatableObject
    {

        [Required(ErrorMessage = "StockSymbol missing")]
        public string? StockSymbol { get; set; }

        [Required(ErrorMessage = "StockSymbol missing")]
        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000, ErrorMessage = "Range should be 1 ~ 100000")]
        public uint Quantity { get; set; }

        [Range(1, 10000, ErrorMessage = "Range should be 1 ~ 10000")]
        public double Price { get; set; }

        /// <summary>
        /// Model class-level validation using IValidatableObject
        /// </summary>
        /// <param name="validationContext">ValidationContext to validate</param>
        /// <returns>Returns validation errors as ValidationResult</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (DateAndTimeOfOrder < Convert.ToDateTime("2000-01-01"))
            {
                results.Add(new ValidationResult("Date of the order should not be older than Jan 01, 2000."));
            }

            return results;
        }

        /// <summary>
        /// Convert the current object from BuyOrderRequest to BuyOrder
        /// </summary>
        /// <returns>
        /// Return the BuyOrder with current BuyOrderRequest details
        /// </returns>
        public BuyOrder ToBuyOrder() 
        {
            return new BuyOrder()
            {
                StockSymbol = StockSymbol,
                StockName = StockName,
                DateAndTimeOfOrder = DateAndTimeOfOrder,
                Price = Price,
                Quantity= Quantity
            };
        }
    }
}
