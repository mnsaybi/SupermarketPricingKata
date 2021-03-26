using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketPricing.Models
{
    class Order
    {
        /// <summary>
        /// Order operation identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// List of products/orders items or operations
        /// </summary>
        public IList<ProductOrder> ProductOrderList { get; set; }

        /// <summary>
        /// TotalPrice of products orders
        /// </summary>
        public decimal TotalPrice { get; set; }


    }
}
