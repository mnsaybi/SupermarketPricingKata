using SupermarketPricing.Common;
using SupermarketPricing.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketPricing.Business
{
    class OrderBusiness : IOrderBusiness
    {

        public OrderBusiness()
        {
        }


        public decimal CalculateTotalPrice(Order order)
        {
            try
            {
                decimal totalPrice = 0;
                if (order == null)
                {
                    throw new ArgumentNullException();
                }

                IList<ProductOrder> orderItems = order.ProductOrderList;

                if (orderItems.Count == 0) return 0;

                foreach (var item in orderItems)
                {
                    totalPrice += CalculatePriceForProductOrder(item);
                }

                return totalPrice;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private decimal CalculatePriceForProductOrder(ProductOrder item)
        {
            decimal price = 0;
            var pricingRule = item.Product.PricingRule;

            if (pricingRule == null)
            {
                // standard pricing
                price = item.Quantity * item.Product.UnitPrice;
            }
            else
            {
                if (pricingRule.Quantity <= 0)
                {
                    throw new ArgumentOutOfRangeException("Cannot calculate price with invalid quantity on pricing rule");
                }

                if (pricingRule.Price <= 0)
                {
                    throw new ArgumentOutOfRangeException("Cannot calculate price with invalid pricing rule");
                }

                var nbrOfUnits = UnitsConverter.GetEquivalentUnit(item.Product.MeasureUnit, pricingRule.MeasureUnit);
                //price = item.Quantity * item.Product.UnitPrice;

                // get the total qunatity after unit conversion
                var totalQty = item.Quantity * nbrOfUnits;

                // calculate the number of discounts corresponding to pricing rule
                var nbrOfDiscounts = Math.Floor(totalQty / pricingRule.Quantity);

                // calculate the remaing quantity price
                // may be calculated with applied pricing rule or with initial price
                var restQty = totalQty % pricingRule.Quantity;

                price = nbrOfDiscounts * pricingRule.Price;

                // calculate the price of remaing quantity depending on the pricing rule
                if (item.Product.UnitPrice > 0)
                {
                    price += restQty * item.Product.UnitPrice;
                }
                else
                {
                    // calculate the unitprice from princing rule
                    price += restQty * (pricingRule.Price / pricingRule.Quantity);
                }
            }

            return price;

        }

    }
}
