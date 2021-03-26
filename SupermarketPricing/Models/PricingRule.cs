using System;
using System.Collections.Generic;
using System.Text;
using SupermarketPricing.Common;

namespace SupermarketPricing.Models
{
    public class PricingRule
    {
        /// <summary>
        /// Pricing rule identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The pricing rule description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The quantity of products corresponding to applied price.
        /// </summary>
        public decimal Quantity { get; set; }

         /// <summary>
         /// The bonus quantity of products offered for each ordere.
         /// </summary>
        public decimal BonusQuantity { get; set; }

        /// <summary>
        /// The applied price corresponding to the quantity.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Measure unit corresponding to product.
        /// </summary>
        public MeasureUnit MeasureUnit { get; set; }

    }
}
