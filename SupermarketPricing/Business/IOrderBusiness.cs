using SupermarketPricing.Models;

namespace SupermarketPricing.Business
{
    internal interface IOrderBusiness
    {
        /// <summary>
        /// Add new product order item to existing order
        /// </summary>
        /// <param name="order">The current order</param>
        /// <param name="item">The new item to be added</param>
        /// <returns>The order object with the new item</returns>
        Order AddItemToOrder(Order order, ProductOrder item);

        /// <summary>
        /// Calculate the total price for order 
        /// </summary>
        /// <param name="order">The order with all items to calculate</param>
        /// <returns>A decimal value representing the total price</returns>
        decimal CalculateTotalPrice(Order order);


    }
}