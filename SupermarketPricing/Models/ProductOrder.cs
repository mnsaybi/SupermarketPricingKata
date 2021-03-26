using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketPricing.Models
{
    class ProductOrder
    {
        /// <summary>
        /// Product order operation identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The order operation product.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Quantity of product for current order.
        /// </summary>
        public decimal Quantity { get; set; }

    }
}
