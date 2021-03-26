using System;
using System.Collections.Generic;
using System.Text;
using SupermarketPricing.Common;

namespace SupermarketPricing.Models
{
    public class Product
    {
        /// <summary>
        /// Product identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Stock Keeping Unit of the product
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product's unit price
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Pricing rule corresponding or applied to product.
        /// </summary>
        public PricingRule PricingRule { get; set; }

        /// <summary>
        /// Measure unit of product.
        /// </summary>
        public MeasureUnit MeasureUnit { get; set; }

    }
}
